using DataAccessLayer.Models;

namespace PresentationLayer.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ManagerId { get; set; }
        public Instructor Manager { get; set; }
        public DateTime? Manager_HireDate { get; set; }
    }
}
