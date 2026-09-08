using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public int ?DepartmentId { get; set; }
        public Department? Department { get; set; }

        public IList<Course>? Courses { get; set; }
    }
}
