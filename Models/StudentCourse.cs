﻿namespace LectureAPI.Models
{
    public class StudentCourse
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Course Course { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}
