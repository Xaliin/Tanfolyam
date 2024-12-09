using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Models.Data.Classes
{
    public class Headcount : IHeadcount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ICollection<IStudent> Students { get; set; }
        public ICourse Course { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public Headcount(ICourse course)
        {
            Students = new List<IStudent>();
            Course = course;
            Minimum = TanfolyamConstants.HeadcountMinimum;
            Maximum = TanfolyamConstants.HeadcountMaximum;
        }

        public Headcount()
        {
            
        }
    }
}
