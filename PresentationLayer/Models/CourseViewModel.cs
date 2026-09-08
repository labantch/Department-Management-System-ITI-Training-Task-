namespace PresentationLayer.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinDegree { get; set; }
        public int DepartmentId { get; set; }


        public int InstructorId { get; set; }
    }
}
