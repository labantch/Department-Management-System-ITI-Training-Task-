using DataAccessLayer.DBContext;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Diagnostics;
using System.Linq;
using DataAccessLayer.Models;
using BLogicLayer.Interfaces;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITIDbContext _dbContext;
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;

        public HomeController(ITIDbContext dbContext, ICourseService courseService, IEnrollmentService enrollmentService)
        {
            _dbContext = dbContext;
            _courseService = courseService;
            _enrollmentService = enrollmentService;
        }

        public IActionResult Index()
        {
            TempData["InstructorCount"] = _dbContext.Instructors.Count();
            TempData["DepartmentCount"] = _dbContext.Departments.Count();
            TempData["CourseCount"] = _dbContext.Courses.Count();
            TempData["TraineeCount"] = _dbContext.Trainees.Count();
            TempData["EnrollmentCount"] = _dbContext.Enrollments.Count();

            // Compute overall track capacity = enrolled trainees / total trainees (as %)
            var totalEnrollments = _dbContext.Enrollments.Count();
            var totalTrainees = Math.Max(1, _dbContext.Trainees.Count());
            var capacityPercent = Math.Round((double)totalEnrollments / totalTrainees * 100);
            if (capacityPercent > 100) capacityPercent = 100;
            ViewBag.TrackCapacityPercent = capacityPercent;

            // Pass recent enrollments to the dashboard view so the Recent Enrollments area is populated
            var recent = _enrollmentService.GetAll().OrderByDescending(e => e.CourseId).Take(5).ToList();
            return View(recent);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
