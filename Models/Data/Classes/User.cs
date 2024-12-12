using Microsoft.AspNetCore.Identity;

namespace Tanfolyam.Models.Data.Classes
{
    public class User : IdentityUser
    {
        public ICollection<Enrollment> Enrollments { get; set; }
        public double Budget { get; set; }

        public User()
        {
            Enrollments = new List<Enrollment>();
        }
    }
}