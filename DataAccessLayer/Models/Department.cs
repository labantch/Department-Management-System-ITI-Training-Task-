using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int? ManagerId { get; set; }
        public Instructor? Manager { get; set; }
        public DateTime? Manager_HireDate { get; set; }
        [InverseProperty("Department")]
        public IList<Instructor> Instructors { get; set; }

        public IList<Course> Courses { get; set; }

    }
}
