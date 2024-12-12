using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Tanfolyam.Models.Data.Classes
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int CourseId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}
