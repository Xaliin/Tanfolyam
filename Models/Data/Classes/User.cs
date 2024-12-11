using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Tanfolyam.Models.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Tanfolyam.Models.Data.Classes
{
    public class User : IdentityUser
    {
        public ICollection<Course> Courses { get; set; }
        public double Budget { get; set; }
    }
}