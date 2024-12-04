namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IStudent
    {
        public int Id { get; set; }
        public ICredentials Credentials { get; set; }
        public ICollection<ICourse> Courses { get; set; }
        public double Budget { get; set; }
    }
}
