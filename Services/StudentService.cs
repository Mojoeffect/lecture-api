using LectureAPI.Data;
using LectureAPI.Interfaces;
using LectureAPI.Models;
using LectureAPI.Dtos;
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

        public async Task<StudentDto> AddStudentAsync(StudentDto studentDto)
        {
            if(studentDto == null)
            {
                throw new ArgumentNullException(nameof(studentDto), "Student cannot be null");
            }
            Student student = new Student
            {
                Name = studentDto.Name,
                Age = studentDto.Age,
                Gender = studentDto.Gender,
                IsNigeria = studentDto.IsNigeria,
                Id = studentDto.Id,
                Department = studentDto.Department,
                Faculty = studentDto.Faculty,
                Level = studentDto.Level,
                MatricNumber = studentDto.MatricNumber
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return studentDto; 
        }

        

        public async Task<StudentDto> GetStudentAsync(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if(student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} doesn't exist");
            }
            var studentDto = new StudentDto
            {
                Name = student.Name,
                Age = student.Age,
                Gender = student.Gender,
                IsNigeria = student.IsNigeria,
                Id = student.Id,
                Department = student.Department,
                Faculty = student.Faculty,
                Level = student.Level,
                MatricNumber = student.MatricNumber
            };
            
            return studentDto;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
        {
            var students =  await _context.Students.ToListAsync();

            var studentsDto = students.Select(student => new StudentDto
            {
                Name = student.Name,
                Age = student.Age,
                Gender = student.Gender,
                IsNigeria = student.IsNigeria,
                Id = student.Id,
                Department = student.Department,
                Faculty = student.Faculty,
                Level = student.Level,
                MatricNumber = student.MatricNumber
            });
            return studentsDto;
        }

        public async Task<StudentDto> UpdateStudentAsync(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                throw new ArgumentNullException(nameof(studentDto), "Student cannot be null");
            }
            var existingStudent = await _context.Students.FindAsync(studentDto.Id);
            if (existingStudent == null)
            {
                throw new KeyNotFoundException($"Student with ID {studentDto.Id} not found");
            }

            Student updatedStudent = new Student
            {
                Name = studentDto.Name,
                Age = studentDto.Age,
                Gender = studentDto.Gender,
                IsNigeria = studentDto.IsNigeria,
                Id = studentDto.Id,
                Department = studentDto.Department,
                Faculty = studentDto.Faculty,
                Level = studentDto.Level,
                MatricNumber = studentDto.MatricNumber
            };

            _context.Entry(existingStudent).CurrentValues.SetValues(updatedStudent);
            await _context.SaveChangesAsync();
            return studentDto;


        }
        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found");
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
