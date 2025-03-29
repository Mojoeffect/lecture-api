using LectureAPI.Models;
using System.Collections;
using LectureAPI.Dtos;
namespace LectureAPI.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> AddStudentAsync(StudentDto studentDto);
        Task<StudentDto> GetStudentAsync(int id);
        Task<IEnumerable<StudentDto>> GetStudentsAsync();
        Task<StudentDto> UpdateStudentAsync(StudentDto studentDto);
        Task DeleteStudentAsync(int id);

    }
}
