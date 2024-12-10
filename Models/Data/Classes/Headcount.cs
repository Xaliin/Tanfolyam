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
        public ICollection<IUser> Students { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public Headcount()
        {
            Students = new List<IUser>();
            Minimum = TanfolyamConstants.HeadcountMinimum;
            Maximum = TanfolyamConstants.HeadcountMaximum;
        }
    }
}
