using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tanfolyam.Data;
using Tanfolyam.Models.Data.Enums;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Models.Data.Classes
{
    public class Course : ICourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseType Type { get; set; }
        public ITeacher Teacher { get; }
        public IHeadcount Headcount { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ISchedule Schedule { get; set; }
        public Status Status { get; set; }

        public Course(string name, CourseType type, ITeacher teacher, string description, double price, ISchedule schedule)
        {
            Headcount headcount = new(this);

            this.Name = name;
            this.Type = type;
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
