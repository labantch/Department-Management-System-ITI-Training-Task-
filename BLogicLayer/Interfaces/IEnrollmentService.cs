using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Interfaces
{
    public interface IEnrollmentService
    {
        List<Enrollment> GetAll();
        bool Add(Enrollment enrollment);
        bool DeleteByCourseId(int courseId);
        bool Delete(int traineeId, int courseId);

    }
}
