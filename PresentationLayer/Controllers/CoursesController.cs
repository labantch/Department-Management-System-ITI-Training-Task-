using BLogicLayer.Interfaces;
using BLogicLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;




namespace PresentationLayer.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IDepartmentService _departmentService;
        private readonly IInstructorService _instructorService;

        public CoursesController(ICourseService courseService, IDepartmentService departmentService, IInstructorService instructorService)
        {
            _courseService = courseService;
            _departmentService = departmentService;
            _instructorService = instructorService;
        }


        public IActionResult Index()
        {
            var courses = _courseService.GetAll();
            return View("~/Views/CourseViews/Index.cshtml", courses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentService.GetAll();
            var instructors = _instructorService.GetAll();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            ViewBag.Instructors = new SelectList(instructors, "Id", "Name");
            return View("~/Views/CourseViews/Create.cshtml");
        }
        [HttpPost]
        public IActionResult Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Id = model.Id,
                    Name = model.Name,
                    MinDegree = model.MinDegree,
                    DepartmentId = model.DepartmentId,
                    InstructorId = model.InstructorId



                };
                bool isAdded = _courseService.Add(course);
                if (isAdded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to add the course.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();

            ViewBag.Departments = new SelectList(_departmentService.GetAll(), "Id", "Name", course.DepartmentId);
            ViewBag.Instructors = new SelectList(_instructorService.GetAll(), "Id", "Name", course.InstructorId);

            return View("~/Views/CourseViews/Edit.cshtml", course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            ModelState.Remove("Department");
            ModelState.Remove("Instructor");
            ModelState.Remove("Trainees");
            ModelState.Remove("Enrollments");

            if (ModelState.IsValid)
            {
                _courseService.Update(course);
                return RedirectToAction("Index");
            }

            ViewBag.Departments = new SelectList(_departmentService.GetAll(), "Id", "Name", course.DepartmentId);
            ViewBag.Instructors = new SelectList(_instructorService.GetAll(), "Id", "Name", course.InstructorId);

            // Explicitly point to your folder path here too
            return View("~/Views/CourseViews/Edit.cshtml", course);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _courseService.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }


    }
}
