using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tanfolyam.Models.Data.Enums;

namespace Tanfolyam.Models.Data.Classes
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseType Type { get; set; }
        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher? Teacher { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StudentCount { get; set; }
        public Schedule Schedule { get; set; }
        public Status Status { get; set; }

        public Course(string name, CourseType type, Teacher teacher, string description, double price, Schedule schedule)
        {
            this.Name = name;
            this.Type = type;
            this.TeacherId = teacher.Id;
            this.Teacher = teacher;
            this.Description = description;
            this.Price = price;
            this.StudentCount = 0;
            this.Schedule = schedule;
            this.Status = Status.Inactive;
        }

        public Course()
        {

        }
    }
}
