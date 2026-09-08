using BLogicLayer.Interfaces;
using BLogicLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace PresentationLayer.Controllers
{
    public class InstructorsController: Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly IDepartmentService _departmentService;

        public InstructorsController(IInstructorService instructorService, IDepartmentService departmentService)
        {
            _instructorService = instructorService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var instructors = _instructorService.GetAll();
            ViewBag.AllDepartments = _departmentService.GetAll();
            return View("~/Views/InstructorViews/Index.cshtml", instructors);
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor = _instructorService.GetById(id);
            if (instructor == null)
            {
                return NotFound();
            }

            var departments = _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", instructor.DepartmentId);

            return View("~/Views/InstructorViews/Edit.cshtml", instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            ModelState.Remove("Department");
            ModelState.Remove("Courses");
            if (ModelState.IsValid)
            {
                _instructorService.Update(instructor);
                return RedirectToAction("Index");
            }

            var departments = _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", instructor.DepartmentId);

            return View("~/Views/InstructorViews/Edit.cshtml", instructor);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var instructor = _instructorService.GetById(id);
            var managedDepartments = _departmentService.GetAll().Where(d => d.ManagerId == id).ToList();

            foreach (var dept in managedDepartments)
            {
                dept.ManagerId = null;
                _departmentService.Update(dept);
            }
            if (instructor == null)
            {
                return NotFound();
            }
            _instructorService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View("~/Views/InstructorViews/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            ModelState.Remove("Department");
            ModelState.Remove("Courses");
            ModelState.Remove("Trainees"); 

            if (ModelState.IsValid)
            {
                
                _instructorService.Add(instructor);
                return RedirectToAction("Index");
            }

         
            var departments = _departmentService.GetAll();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", instructor.DepartmentId);

            return View("~/Views/InstructorViews/Create.cshtml", instructor);
        }





    }
}
