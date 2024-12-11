using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tanfolyam.Data;
using Tanfolyam.Models.Data.Enums;
using Tanfolyam.Models.Data.Interfaces;

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
        public Headcount Headcount { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Schedule Schedule { get; set; }
        public Status Status { get; set; }

        public Course(string name, CourseType type, Teacher teacher, string description, double price, Schedule schedule)
        {
            Headcount headcount = new();

            this.Name = name;
            this.Type = type;
            this.TeacherId = teacher.Id;
            this.Teacher = teacher;
            this.Headcount = headcount;
            this.Description = description;
            this.Price = price;
            this.Schedule = schedule;
            this.Status = Status.Inactive;
        }

        public Course()
        {
            
        }
    }
}
