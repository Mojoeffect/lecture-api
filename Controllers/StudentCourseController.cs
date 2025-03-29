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
            var studentCourseDto = new StudentCourseDto
            {
                CourseId = studentCourse.CourseId,
                StudentId = studentCourse.StudentId
            };
            return Ok(studentCourseDto);
        }

        [HttpDelete("{studentId} {courseId}")]
        public async Task<IActionResult> RemoveStudentCourse(int studentId, int courseId)
        {
            await _studentCourseService.RemoveStudentCourseAsync(studentId, courseId);
            return Ok();
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetCoursesForStudent(int studentId)
        {
            var coursesForStudent = await _studentCourseService.GetCoursesForStudentAsync(studentId);
            if (coursesForStudent == null)
            {
                return NotFound();
            }
            var coursesForStudentDto = coursesForStudent.Select(course => new CourseDto
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseTitle = course.CourseTitle,
                CreditUnit = course.CreditUnit
            });

            return Ok(coursesForStudentDto);
        }

        [HttpGet("/{courseId}")]
        public async Task<IActionResult> GetStudentsForCourse(int courseId)
        {
            var studentsForCourse = await _studentCourseService.GetStudentsForCourseAsync(courseId);
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
            if(studentsForCourse == null)
            {
                return NotFound();
            }
            return Ok(studentsForCourseDto);
        }
    }
}
