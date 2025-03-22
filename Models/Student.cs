namespace LectureAPI.Models
{
    public class Student : Person
    {
        public int Id { get; set; }
        public string? Department { get; set; }
        public string? Faculty { get; set; }
        public string? Level { get; set; }
        public string? MatricNumber { get; set; }
        public List<Course>? Courses { get; set; }

    }
}
