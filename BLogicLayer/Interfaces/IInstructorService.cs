using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Interfaces
{
    public interface IInstructorService
    {
        List<Instructor> GetAll();
        Instructor GetById(int id);

        bool Add(Instructor instructor);
        bool Update(Instructor instructor);
        bool Delete(int id);
    }
}
