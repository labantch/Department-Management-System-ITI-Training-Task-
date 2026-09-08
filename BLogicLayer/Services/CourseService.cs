using BLogicLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace BLogicLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly ITIDbContext _service;

        public CourseService(ITIDbContext service)
        {
            _service = service;
        }
        public bool Add(Course course)
        {
            if (course == null) return false;
             _service.Courses.Add(course);
            _service.SaveChanges(); 
            return true;
        }

        public bool Delete(int id)
        {
            var course = _service.Courses.Find(id);
            if (course == null) return false;
            _service.Courses.Remove(course);
            _service.SaveChanges();
            return true;
        }

        public List<Course> GetAll()
        {
            return _service.Courses.Include(c => c.Department).Include(c=>c.Instructor).ToList();
        }

        public Course? GetById(int id)
        {
            var course = _service.Courses.Find(id);
            if (course == null) return null;
            return course;
        }

        public bool Update(Course course)
        {
            if(course == null) return false;

            var Course = _service.Courses.Update(course);
            _service.SaveChanges();
            return true;

        }
    }
}
