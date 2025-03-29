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
            Student student = new Student
            {
                Name = studentDto.Name,
                Age = studentDto.Age,
                Gender = studentDto.Gender,
                IsNigeria = studentDto.IsNigeria,
                Id = studentDto.Id,
                Department = studentDto.Department,
                Faculty = studentDto.Faculty,
                Level = studentDto.Level,
                MatricNumber = studentDto.MatricNumber
            };
            await _studentService.AddStudentAsync(student);
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
            var studentDto = new StudentDto
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
            };
            return Ok(studentDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudentsAsync();
            var studentsDto = students.Select(student => new StudentDto
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
            return Ok(studentsDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDto updatedStudentDto)
        {
            if(id != updatedStudentDto.Id)
            {
                return BadRequest("ID mismatch");
            }
            var updatedStudent = new Student
            {
                Name = updatedStudentDto.Name,
                Age = updatedStudentDto.Age,
                Gender = updatedStudentDto.Gender,
                IsNigeria = updatedStudentDto.IsNigeria,
                Id = updatedStudentDto.Id,
                Department = updatedStudentDto.Department,
                Faculty = updatedStudentDto.Department,
                Level = updatedStudentDto.Level,
                MatricNumber = updatedStudentDto.MatricNumber
            };

            await _studentService.UpdateStudentAsync(updatedStudent);
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
