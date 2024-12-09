using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Models.Data.Classes
{
    public class Teacher : ITeacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ICourse> Courses { get; }

        public Teacher(string name)
        {
            this.Name = name;
            Courses = new List<ICourse>();
        }

        public Teacher()
        {
            
        }
    }
}
