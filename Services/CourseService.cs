using LectureAPI.Interfaces;
using LectureAPI.Data;
using LectureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LectureAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Course> AddCourseAsync(Course course)
        {
            if(course == null)
            {
                throw new ArgumentNullException(nameof(course), "Course cannot be null");
            }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }
        public async Task<Course> GetCourseAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }
        public async Task<Course> UpdateCourseAsync(Course course)
        {
            if(course == null)
            {
                throw new ArgumentNullException(nameof(course), "Course cannot be null");
            }
            var existingCourse = await _context.Courses.FindAsync(course.Id);
            if(existingCourse == null)
            {
                throw new KeyNotFoundException($"Course with ID {course.Id} not found");
            }
            _context.Entry(existingCourse).CurrentValues.SetValues(course);
            await _context.SaveChangesAsync();
            return course;
        }
        public async Task<Course> UpdateCourseTitleAsync(int id, string? newTitle)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if(existingCourse == null)
            {
                throw new KeyNotFoundException($"Course with id {existingCourse.Id} does not exist");
            }

            _context.Entry(existingCourse.CourseTitle).CurrentValues.SetValues(newTitle);
            await _context.SaveChangesAsync();
            return existingCourse;
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if(course == null)
            {
                throw new KeyNotFoundException($"Course with ID {course.Id} not found");
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
