using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tanfolyam.Models.Data.Classes
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public Teacher(string name)
        {
            this.Name = name;
        }

        public Teacher()
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
