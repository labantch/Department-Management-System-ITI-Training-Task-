using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Linq;

// Use enrollment and trainee services to actually persist enrollments

namespace PresentationLayer.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ITraineeSevice _traineeService;

        public EnrollmentsController(ICourseService courseService, IEnrollmentService enrollmentService, ITraineeSevice traineeService)
        {
            _courseService = courseService;
            _enrollmentService = enrollmentService;
            _traineeService = traineeService;
        }

        public IActionResult Index()
        {
            // Show real enrollment records (with Trainee and Course loaded)
            var enrollments = _enrollmentService.GetAll();
            return View("~/Views/EnrollmentViews/Index.cshtml", enrollments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name");
            return View("~/Views/EnrollmentViews/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(string TraineeName, string TraineeEmail, int CourseId, string Status)
        {
            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", CourseId);

            if (string.IsNullOrWhiteSpace(TraineeName) || CourseId <= 0)
            {
                ModelState.AddModelError("", "Please provide valid trainee information and select a course track.");
                return View("~/Views/EnrollmentViews/Create.cshtml");
            }

            // Try to find existing trainee by name (fallback) - the Trainee model currently doesn't have an Email field
            var trainee = _traineeService.GetAll().FirstOrDefault(t => t.Name.Equals(TraineeName?.Trim() ?? string.Empty, StringComparison.OrdinalIgnoreCase));
            if (trainee == null)
            {
                trainee = new Trainee
                {
                    Name = TraineeName.Trim(),
                    // store provided email in Address temporarily if there is no Email column yet
                    Address = TraineeEmail?.Trim() ?? string.Empty
                };

                var added = _traineeService.Add(trainee);
                if (!added)
                {
                    ModelState.AddModelError("", "Failed to create trainee record.");
                    return View("~/Views/EnrollmentViews/Create.cshtml");
                }
                // Ensure we have the DB-generated Id. Some providers may not set the Id on the passed object,
                // so re-query the trainee by name to get the persisted entity with Id populated.
                if (trainee.Id == 0)
                {
                    var existing = _traineeService.GetAll().FirstOrDefault(t => t.Name.Equals(trainee.Name, StringComparison.OrdinalIgnoreCase));
                    if (existing != null)
                    {
                        trainee = existing;
                    }
                }
            }

            var enrollment = new Enrollment
            {
                TraineeId = trainee.Id,
                CourseId = CourseId,
                Degree = 0,
                Grade = null
            };
            var already = _enrollmentService.GetAll().Any(e => e.TraineeId == enrollment.TraineeId && e.CourseId == enrollment.CourseId);
            if (already)
            {
                ModelState.AddModelError("", "This trainee is already enrolled in the selected course.");
                return View("~/Views/EnrollmentViews/Create.cshtml");
            }

            var result = _enrollmentService.Add(enrollment);
            if (!result)
            {
                ModelState.AddModelError("", "Failed to create enrollment.");
                return View("~/Views/EnrollmentViews/Create.cshtml");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int traineeId, int courseId)
        {
            var deleted = _enrollmentService.Delete(traineeId, courseId);
            if (!deleted)
            {
                TempData["Error"] = "No enrollment found for the specified trainee/course.";
            }
            return RedirectToAction("Index");
        }
    }
}