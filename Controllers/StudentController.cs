using LectureAPI.Dtos;
using LectureAPI.Interfaces;
using LectureAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentDto studentDto)
        {
            await _studentService.AddStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetStudent), new { id = studentDto.Id }, studentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudentsAsync();
            return Ok(students);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDto updatedStudentDto)
        {
            if(id != updatedStudentDto.Id)
            {
                return BadRequest("ID mismatch");
            }
            
            await _studentService.UpdateStudentAsync(updatedStudentDto);
            return Ok(updatedStudentDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
    }

    
}
