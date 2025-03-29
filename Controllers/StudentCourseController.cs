using LectureAPI.Dtos;
using LectureAPI.Interfaces;
using LectureAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseServiceController : ControllerBase
    {
        private readonly IStudentCourseService _studentCourseService;
        public StudentCourseServiceController(IStudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentCourse(int studentId, int courseId)
        {
            var studentCourse = await _studentCourseService.AddStudentCourseAsync(studentId, courseId);
            return Ok(studentCourse);
        }

        [HttpDelete("{studentId} {courseId}")]
        public async Task<IActionResult> RemoveStudentCourse(int studentId, int courseId)
        {
            await _studentCourseService.RemoveStudentCourseAsync(studentId, courseId);
            return NoContent();
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetCoursesForStudent(int studentId)
        {
            var coursesForStudent = await _studentCourseService.GetCoursesForStudentAsync(studentId);
            if (coursesForStudent == null)
            {
                return NotFound();
            }
            return Ok(coursesForStudent);
        }

        [HttpGet("/{courseId}")]
        public async Task<IActionResult> GetStudentsForCourse(int courseId)
        {
            var studentsForCourse = await _studentCourseService.GetStudentsForCourseAsync(courseId);
            if(studentsForCourse == null)
            {
                return NotFound();
            }

            return Ok(studentsForCourse);
        }
    }
}
