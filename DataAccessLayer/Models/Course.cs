using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MinDegree { get; set; }
        public int? DepartmentId { get; set; }


        public int? InstructorId { get; set; }
        public Department? Department { get; set; }
        public Instructor? Instructor { get; set; }

        public IList<Trainee>? Trainees { get; set; }
    }
}