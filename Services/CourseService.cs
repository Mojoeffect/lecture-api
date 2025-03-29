using LectureAPI.Interfaces;
using LectureAPI.Data;
using LectureAPI.Models;
using LectureAPI.Dtos;
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
        public async Task<CourseDto> AddCourseAsync(CourseDto courseDto)
        {
            if(courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto), "Course cannot be null");
            }

            Course course = new Course
            {
                Id = courseDto.Id,
                CourseCode = courseDto.CourseCode,
                CourseTitle = courseDto.CourseTitle,
                CreditUnit = courseDto.CreditUnit,

            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return courseDto;
        }
        public async Task<CourseDto> GetCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {id} doesn't exist");
            }

            CourseDto courseDto = new CourseDto
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle,
                CreditUnit = course.CreditUnit
            };
            return courseDto;
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            var coursesDto = courses.Select(course => new CourseDto
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle,
                CreditUnit = course.CreditUnit
            });
            return coursesDto;
        }
        public async Task<CourseDto> UpdateCourseAsync(CourseDto courseDto)
        {
            if(courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto), "Course cannot be null");
            }
            var existingCourse = await _context.Courses.FindAsync(courseDto.Id);
            if(existingCourse == null)
            {
                throw new KeyNotFoundException($"Course with ID {courseDto.Id} not found");
            }

            Course course = new Course
            {
                Id = courseDto.Id,
                CourseCode = courseDto.CourseCode,
                CourseTitle = courseDto.CourseTitle,
                CreditUnit = courseDto.CreditUnit,
            };
            _context.Entry(existingCourse).CurrentValues.SetValues(course);
            await _context.SaveChangesAsync();

            return courseDto;
        }
        public async Task<CourseDto> UpdateCourseCodeAsync(int id, string? newCourseCode)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if(existingCourse == null)
            {
                throw new KeyNotFoundException($"Course with id {id} does not exist");
            }
            if(newCourseCode == null)
            {
                throw new ArgumentNullException("Course code cannot be null");
            }
            _context.Entry(existingCourse).CurrentValues.SetValues(existingCourse.CourseCode = newCourseCode);
            await _context.SaveChangesAsync();

            CourseDto updatedCourseDto = new CourseDto
            {
                Id = existingCourse.Id,
                CourseCode = existingCourse.CourseCode,
                CourseTitle = existingCourse.CourseTitle,
                CreditUnit = existingCourse.CreditUnit
            };
            return updatedCourseDto;
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if(course == null)
            {
                throw new KeyNotFoundException($"Course with ID {id} not found");
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
