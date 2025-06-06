﻿namespace LectureAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseTitle { get; set; }
        public int CreditUnit { get; set; }
        public List<StudentCourse>? StudentCourses { get; set; } = new List<StudentCourse>(); //for many-many navigation
    }
}
