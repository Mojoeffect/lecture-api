using LectureAPI.Models;
using LectureAPI.Dtos;
namespace LectureAPI.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> AddCourseAsync(CourseDto courseDto);
        Task<CourseDto> GetCourseAsync(int id);
        Task<IEnumerable<CourseDto>> GetCoursesAsync();
        Task<CourseDto> UpdateCourseAsync(CourseDto courseDto);
        Task<CourseDto> UpdateCourseCodeAsync(int id, string? newCode);
        Task DeleteCourseAsync(int id);
    }
}
