using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLogicLayer.Interfaces
{
    public interface ITraineeSevice
    {

        List<Trainee> GetAll();
        Trainee GetById(int id);

        bool Add(Trainee trainee);
        bool Update(Trainee trainee);
        bool Delete(int id);
    }
}
