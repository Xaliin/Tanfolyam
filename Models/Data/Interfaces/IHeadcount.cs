namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IHeadcount
    {
        public int Id { get; set; }
        public ICollection<IStudent> Students { get; set; }
        public ICourse Course { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
    }
}
