namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IUser
    {
        public ICollection<ICourse> Courses { get; set; }
        public double Budget { get; set; }
    }
}
