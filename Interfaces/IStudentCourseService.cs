using LectureAPI.Models;

namespace LectureAPI.Interfaces
{
    public interface IStudentCourseService
    {
        Task<StudentCourse> AddStudentCourseAsync(int studentId, int courseId);
        Task RemoveStudentCourseAsync(int studentId, int courseId);
        Task<IEnumerable<Course>> GetCoursesForStudentAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentsForCourseAsync(int courseId);
    }
}
