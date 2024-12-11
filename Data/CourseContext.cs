using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tanfolyam.Models.Data.Classes;

namespace Tanfolyam.Data
{
    public class CourseContext : IdentityDbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Headcount> Headcount { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Schedule> Schedule { get; set; }

        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
               .HasOne(c => c.Teacher)
               .WithMany(t => t.Courses)
               .HasForeignKey(c => c.TeacherId) 
               .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
