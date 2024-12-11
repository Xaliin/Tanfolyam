using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Models.Data.Classes
{
    public class Headcount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ICollection<User> Students { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public Headcount()
        {
            Students = new List<User>();
            Minimum = TanfolyamConstants.HeadcountMinimum;
            Maximum = TanfolyamConstants.HeadcountMaximum;
        }

        public override string ToString()
        {
            return Students.Count().ToString();
        }
    }
}
