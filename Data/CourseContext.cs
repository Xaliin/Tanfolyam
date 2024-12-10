using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tanfolyam.Models.Data.Classes;

namespace Tanfolyam.Data
{
    public class CourseContext : IdentityDbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Headcount> Headcount { get; set; }
        public DbSet<User> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Schedule> Schedule { get; set; }




        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasOne(c => (Headcount)c.Headcount)
                .WithMany()
                .HasForeignKey("CourseId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Headcount>()
                .HasMany(l => (ICollection<User>)l.Students)
                .WithOne()
                .HasForeignKey("HeadcountId");

            modelBuilder.Entity<User>()
                .HasMany(s => (ICollection<Course>)s.Courses)
                .WithOne()
                .HasForeignKey("StudentId");

            modelBuilder.Entity<Course>()
                .HasOne(c => (Schedule)c.Schedule)
                .WithMany()
                .HasForeignKey("CourseId");
        }
    }
}
