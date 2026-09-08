using System;
using System.Collections.Generic;
using System.Text;
using BLogicLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.DBContext;

namespace BLogicLayer.Services
{
    public class TraineeService : ITraineeSevice
    {
        private readonly ITIDbContext _context;

        public TraineeService(ITIDbContext context)
        {
            _context = context;
        }
        public bool Add(Trainee trainee)
        {
            if (trainee == null) return false;
            _context.Trainees.Add(trainee);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var trainee = _context.Trainees.Find(id);
            if (trainee == null) return false;
            _context.Trainees.Remove(trainee);
            _context.SaveChanges();
            return true;
        }
        

        public List<Trainee> GetAll()
        {
            return _context.Trainees.ToList();
        }

        public Trainee? GetById(int id)
        {
            return _context.Trainees.Find(id);
        }

        public bool Update(Trainee trainee)
        {
            if (trainee == null) return false;
            _context.Trainees.Update(trainee);
            _context.SaveChanges();
            return true;
        }
        }
    }
