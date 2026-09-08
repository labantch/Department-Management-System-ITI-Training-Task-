using BLogicLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Services
{
    public class InstructorService : IInstructorService
    {

        private readonly ITIDbContext _dbContext;

        public InstructorService(ITIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(Instructor instructor)
        {
            if(instructor is null) return false;

            _dbContext.Instructors.Add(instructor);
            _dbContext.SaveChanges();
            return true;

        }

        public bool Delete(int id)
        {
            var instructor = _dbContext.Instructors.Find(id);
            if (instructor == null) return false;

            foreach (var dept in _dbContext.Departments.Where(d => d.ManagerId == id))
            {
                dept.ManagerId = null;
            }

            foreach (var course in _dbContext.Courses.Where(c => c.InstructorId == id))
            {
                course.InstructorId = null;
            }
            _dbContext.Instructors.Remove(instructor);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Instructor> GetAll()
        {
            return _dbContext.Instructors
                   .Include(i => i.Department)
                   .ToList();
        }

        public Instructor? GetById(int id)
        {
            return _dbContext.Instructors.Find(id);
        }

        public bool Update(Instructor instructor)
        {
            if (instructor == null) return false;
            _dbContext.Instructors.Update(instructor);
            _dbContext.SaveChanges();
            return true;
        }
        }
    
}
