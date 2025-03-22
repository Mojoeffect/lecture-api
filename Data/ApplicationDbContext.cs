using Microsoft.EntityFrameworkCore;
using LectureAPI.Models;

namespace LectureAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasMany<Course>(s => s.Courses);
            modelBuilder.Entity<Course>().HasAlternateKey(s => s.CourseTitle);
        }
   
    }
}
