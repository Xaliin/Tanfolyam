namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IStudent
    {
        public ICollection<ICourse> Courses { get; set; }
        public double Budget { get; set; }
    }
}
