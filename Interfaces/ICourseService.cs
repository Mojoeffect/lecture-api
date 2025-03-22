using LectureAPI.Models;

namespace LectureAPI.Interfaces
{
    public interface ICourseService
    {
        Task<Course> AddCourseAsync(Course course);
        Task<Course> GetCourseAsync(int id);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> UpdateCourseAsync(Course course);
        Task<Course> UpdateCourseCodeAsync(int id, string? newCode);
        Task DeleteCourseAsync(int id);
    }
}
