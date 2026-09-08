using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Interfaces
{
    public  interface ICourseService
    {
        List<Course> GetAll();
        Course GetById(int id);

        bool Add(Course course);
        bool Update(Course course);
        bool Delete(int id);
    }
}
