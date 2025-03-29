using LectureAPI.Dtos;
using LectureAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LectureAPI.Models;
using Microsoft.AspNetCore.Http.Features;

namespace LectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseDto courseDto)
        {
            Course course = new Course
            {
                Id = courseDto.Id,
                CourseCode = courseDto.CourseCode,
                CourseTitle = courseDto.CourseTitle,
                CreditUnit = courseDto.CreditUnit,
                
            };

            await _courseService.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetCourse), new { id = courseDto.Id }, courseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            CourseDto courseDto = new CourseDto
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle,
                CreditUnit = course.CreditUnit
            };
            return Ok(courseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetCoursesAsync();
            var coursesDto = courses.Select(course => new CourseDto
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle,
                CreditUnit = course.CreditUnit
            });
            return Ok(coursesDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCourseCode(int id, string newCourseCode)
        {
            var updatedCourse = await _courseService.UpdateCourseCodeAsync(id, newCourseCode);
            CourseDto courseDto = new CourseDto
            {
                Id = updatedCourse.Id,
                CourseCode = updatedCourse.CourseCode,
                CourseTitle = updatedCourse.CourseTitle,
                CreditUnit = updatedCourse.CreditUnit
                
            };
            return Ok(courseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDto courseDto)
        {
            if(id != courseDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            Course course = new Course
            {
                Id = courseDto.Id,
                CourseCode = courseDto.CourseCode,
                CourseTitle = courseDto.CourseTitle,
                CreditUnit = courseDto.CreditUnit,
            };

            await _courseService.UpdateCourseAsync(course);
            return Ok(courseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return Ok();
        }
    }
}
