using LectureAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LectureAPI.Models;

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
        public async Task<IActionResult> AddCourse(Course course)
        {
            var newCourse = await _courseService.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetCourse), new { id = newCourse.Id }, newCourse);
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

        [HttpPatch]
        public async Task<IActionResult> UpdateCourseTitle(int id, string newTitle)
        {
            var updatedCourse = await _courseService.UpdateCourseTitleAsync(id, newTitle);
            return Ok(updatedCourse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse(int id, Course course)
        {
            if(id != course.Id)
            {
                return BadRequest("ID mismatch");
            }
            var updatedCourse = await _courseService.UpdateCourseAsync(course);
            return Ok(updatedCourse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return Ok();
        }
    }
}
