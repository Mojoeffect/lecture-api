using LectureAPI.Models;

namespace LectureAPI.Dtos
{
    public class StudentDto : Person
    {
        public int Id { get; set; }
        public string? Department { get; set; }
        public string? Faculty { get; set; }
        public string? Level { get; set; }
        public string? MatricNumber { get; set; }
    }
}
