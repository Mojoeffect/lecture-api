using LectureAPI.Models;
using LectureAPI.Dtos;

namespace LectureAPI.Interfaces
{
    public interface IStudentCourseService
    {
        Task<StudentCourseDto> AddStudentCourseAsync(int studentId, int courseId);
        Task RemoveStudentCourseAsync(int studentId, int courseId);
        Task<IEnumerable<CourseDto>> GetCoursesForStudentAsync(int studentId);
        Task<IEnumerable<StudentDto>> GetStudentsForCourseAsync(int courseId);
    }
}
