using LectureAPI.Data;
using LectureAPI.Interfaces;
using LectureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LectureAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null");
            }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student; 
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {student.Id} not found");
            }
             _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null");
            }
            var existingStudent = await _context.Students.FindAsync(student.Id);
            if (existingStudent == null)
            {
                throw new KeyNotFoundException($"Student with ID {student.Id} not found");
            }

            _context.Entry(existingStudent).CurrentValues.SetValues(student);
            await _context.SaveChangesAsync();
            return student;
        }
    }
}
