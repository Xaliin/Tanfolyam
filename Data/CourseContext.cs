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
               .HasForeignKey(c => c.TeacherId) // Use TeacherId as the FK
               .OnDelete(DeleteBehavior.Restrict);

            

            /*
            modelBuilder.Entity<Course>()
                .HasOne(c => (Headcount)c.Headcount)
                .WithMany()
                .HasForeignKey("CourseId")
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Course>()
                .HasOne(c => (Teacher)c.Teacher)
                .WithMany(t => (IEnumerable<Course>)t.Courses)
                .HasForeignKey("CourseId");

            modelBuilder.Entity<Course>()
                .HasOne(c => (Schedule)c.Schedule)
                .WithMany()
                .HasForeignKey("CourseId");

            modelBuilder.Entity<Headcount>()
                .HasMany(l => (ICollection<User>)l.Students)
                .WithOne()
                .HasForeignKey("HeadcountId");

            modelBuilder.Entity<User>()
                .HasMany(s => (ICollection<Course>)s.Courses)
                .WithOne()
                .HasForeignKey("UserId");

            modelBuilder.Entity<Teacher>()
                .HasMany(t => (ICollection<Course>)t.Courses)
                .WithOne()
                .HasForeignKey("TeacherId");
            */
        }
    }
}
