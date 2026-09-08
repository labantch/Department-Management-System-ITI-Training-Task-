using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using System.Linq;
using BLogicLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLogicLayer.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ITIDbContext _context;

        public DepartmentService(ITIDbContext context)
        {
            _context = context;
        }

        public bool Add(Department department)
        {
            if (department == null) return false;
            _context.Departments.Add(department);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var department = _context.Departments
            .Include(d => d.Instructors)
            .Include(d => d.Courses)
            .FirstOrDefault(d => d.Id == id);
            if (department == null) return false;
            if (department.Instructors != null)
            {
                foreach (var instructor in department.Instructors)
                {
                    instructor.DepartmentId = null;
                }
            }

            if (department.Courses != null)
            {
                foreach (var course in department.Courses)
                {
                    course.DepartmentId = null;
                }
            }
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return true;
        }

        public List<Department> GetAll()
        {
            return _context.Departments.Include(c => c.Manager).ToList();
        }

        

        public Department? GetById(int id)
        {
            return _context.Departments.Find(id);
            
        }


        public List<Instructor> GetInstructorsByDeptId(int id)
        {
            return _context.Instructors
                           .Where(i => i.DepartmentId == id)
                           .ToList();
        }

        public bool Update(Department department)
        {
            if (department == null) return false;
            _context.Departments.Update(department);
            _context.SaveChanges();
            return true;
        }
    }
}
