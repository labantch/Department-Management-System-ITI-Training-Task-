using BLogicLayer.Interfaces;
using BLogicLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;


namespace PresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _service;
        private readonly IInstructorService _instructorService;


        public DepartmentsController(IDepartmentService service, IInstructorService instructorService)
        {
            _service = service;
            _instructorService = instructorService; 
        }
        public ViewResult Index()   
        {
            var departments = _service.GetAll();
            return View("Views/DepartmentViews/Index.cshtml", departments);
           
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _service.GetById(id);
            if (department is null) return NotFound();
            ViewBag.Instructors = new SelectList(_instructorService.GetAll(), "Id", "Name", department.ManagerId);
            return View("~/views/Departmentviews/edit.cshtml",department);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            ModelState.Remove("Instructors");
            ModelState.Remove("Courses");
            if (ModelState.IsValid)
            {
                _service.Update(department);
                return RedirectToAction("Index");
            }


            return View("~/views/Departmentviews/edit.cshtml",department);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Instructors = new SelectList(_instructorService.GetAll(), "Id", "Name");
            return View("~/Views/DepartmentViews/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            ModelState.Remove("Manager");
            ModelState.Remove("Instructors");
            ModelState.Remove("Courses");

            if (ModelState.IsValid)
            {
                _service.Add(department);
                return RedirectToAction("Index");
            }

            ViewBag.Instructors = new SelectList(_instructorService.GetAll(), "Id", "Name", department.ManagerId);
            return View("~/Views/DepartmentViews/Create.cshtml", department);
        }



    }
}
