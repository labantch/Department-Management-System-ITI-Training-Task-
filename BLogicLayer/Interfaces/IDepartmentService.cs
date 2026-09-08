using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;

namespace BLogicLayer.Interfaces
{
    
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department GetById(int id);

        bool Add(Department department);
        bool Update(Department department);
        bool Delete(int id);
        List<Instructor> GetInstructorsByDeptId(int id);
    }
}
