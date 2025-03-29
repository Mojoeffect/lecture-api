using LectureAPI.Interfaces;
using LectureAPI.Models;
using LectureAPI.Data;
using LectureAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LectureAPI.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly ApplicationDbContext _context;
        public StudentCourseService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<StudentCourseDto> AddStudentCourseAsync(int studentId, int courseId)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);
            if (!studentExists)
            {
                throw new KeyNotFoundException($"Student with ID {studentId} doesn't exist");
            }
            var courseExists = await _context.Courses.AnyAsync(s => s.Id == courseId);
            if (!courseExists)
            {
                throw new KeyNotFoundException($"Course with ID {courseId} doesn't exist");
            }

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();

            var studentCourseDto = new StudentCourseDto
            {
                CourseId = studentCourse.CourseId,
                StudentId = studentCourse.StudentId
            };
            return studentCourseDto;
        }

        public async Task RemoveStudentCourseAsync(int studentId,int courseId)
        {
            var studentCourse = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if (studentCourse == null)
            {
                throw new KeyNotFoundException($"Student with ID {studentId} is not enrolled in course {courseId}");
            }
            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<CourseDto>> GetCoursesForStudentAsync(int studentId)
        {
            var coursesForStudent = await _context.StudentCourses
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.Course)
                .ToListAsync();

            var coursesForStudentDto = coursesForStudent.Select(course => new CourseDto
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle,
                CreditUnit = course.CreditUnit
            });

            return coursesForStudentDto;
        }
        public async Task<IEnumerable<StudentDto>> GetStudentsForCourseAsync(int courseId)
        {
            var studentsForCourse = await _context.StudentCourses
                .Where(sc => sc.CourseId == courseId)
                .Select(sc => sc.Student)
                .ToListAsync();

            var studentsForCourseDto = studentsForCourse.Select(student => new StudentDto
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

            return studentsForCourseDto;
        }
    }
}
