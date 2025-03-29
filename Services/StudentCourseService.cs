using LectureAPI.Interfaces;
using LectureAPI.Models;
using LectureAPI.Data;
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
        public async Task<StudentCourse> AddStudentCourseAsync(int studentId, int courseId)
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
            return studentCourse;
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
        public async Task<IEnumerable<Course>> GetCoursesForStudentAsync(int studentId)
        {
            return await _context.StudentCourses
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.Course)
                .ToListAsync();
        }
        public async Task<IEnumerable<Student>> GetStudentsForCourseAsync(int courseId)
        {
            return await _context.StudentCourses
                .Where(sc => sc.CourseId == courseId)
                .Select(sc => sc.Student)
                .ToListAsync();
        }
    }
}
