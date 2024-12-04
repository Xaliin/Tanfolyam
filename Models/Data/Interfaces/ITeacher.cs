namespace Tanfolyam.Models.Data.Interfaces
{
    public interface ITeacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ICourse> Courses { get; }
    }
}
