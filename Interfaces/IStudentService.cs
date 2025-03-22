using LectureAPI.Models;
using System.Collections;
namespace LectureAPI.Interfaces
{
    public interface IStudentService
    {
        Task<Student> AddStudentAsync(Student student);
        Task<Student> GetStudentAsync(int id);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);

    }
}
