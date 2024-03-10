using Ergasia2mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace Ergasia2mvc.Data
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseHasStudents>().HasKey(key => new { key.CourseID, key.StudentID });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseHasStudents> CourseHasStudents { get; set; }
    }
}
