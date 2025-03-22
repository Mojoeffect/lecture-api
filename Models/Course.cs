namespace LectureAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseTitle { get; set; }
        public int CreditUnit { get; set; }
        //public List<Student>? Students { get; set; }
    }
}
