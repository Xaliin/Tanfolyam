using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tanfolyam.Models.Data.Classes
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId => User.Id;
        public int CourseId => Course.Id;
        public User? User { get; set; }
        public Course? Course { get; set; }
    }
}
