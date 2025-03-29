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
            await _courseService.AddCourseAsync(courseDto);
            return CreatedAtAction(nameof(GetCourse), new { id = courseDto.Id }, courseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if(course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetCoursesAsync();
            return Ok(courses);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCourseCode(int id, string newCourseCode)
        {
            var updatedCourse = await _courseService.UpdateCourseCodeAsync(id, newCourseCode);
            return Ok(updatedCourse);
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

            await _courseService.UpdateCourseAsync(courseDto);
            return Ok(courseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
