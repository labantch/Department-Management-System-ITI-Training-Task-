using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Enrollment
    {
        public int TraineeId { get; set; }
        public int CourseId { get; set; }

        public int? Grade { get; set; }

        public int Degree { get; set; }

        public Trainee Trainee { get; set; }
        public Course Course { get; set; }
    }
}
