using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Models.Data.Classes
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course>? Courses { get; set; }

        public Teacher(string name)
        {
            this.Name = name;
            Courses = new List<Course>();
        }

        public Teacher()
        {
            Courses = new List<Course>();
        }
    }
}
