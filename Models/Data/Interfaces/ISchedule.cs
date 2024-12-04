namespace Tanfolyam.Models.Data.Interfaces
{
    public interface ISchedule
    {
        public int Id { get; set; }
        public double LengthInHour { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
