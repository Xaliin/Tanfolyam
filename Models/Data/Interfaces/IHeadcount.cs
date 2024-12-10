namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IHeadcount
    {
        public int Id { get; set; }
        public ICollection<IUser> Students { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
    }
}
