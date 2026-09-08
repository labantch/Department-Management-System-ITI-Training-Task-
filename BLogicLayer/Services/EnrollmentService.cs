using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BLogicLayer.Interfaces;
using DataAccessLayer.DBContext;
using DataAccessLayer.Models;

namespace BLogicLayer.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ITIDbContext _context;

        public EnrollmentService(ITIDbContext context)
        {
            _context = context;
        }

        public bool Add(Enrollment enrollment)
        {
           if (enrollment == null) return false;

            var exists = _context.Enrollments.Any(e => e.TraineeId == enrollment.TraineeId && e.CourseId == enrollment.CourseId);
            if (exists) return false;

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
            return true;
        }

        public List<Enrollment> GetAll()
        {
            return _context.Enrollments
                .Include(e => e.Trainee)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Department)
                .ToList();
        }

        public bool DeleteByCourseId(int courseId)
        {
            var list = _context.Enrollments.Where(e => e.CourseId == courseId).ToList();
            if (list.Count == 0) return false;
            _context.Enrollments.RemoveRange(list);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int traineeId, int courseId)
        {
            var enrollment = _context.Enrollments.Find(traineeId, courseId);
            if (enrollment == null) return false;
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
            return true;
        }
    }
}
