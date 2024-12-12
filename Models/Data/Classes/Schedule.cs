using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Models.Data.Classes
{
    public class Schedule
    {
        public int Id { get; set; }
        public double LengthInHour { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public Schedule(double lengthInHour, DateTime registrationDeadline)
        {
            this.LengthInHour = lengthInHour;
            this.RegistrationDeadline = registrationDeadline;
        }
        public Schedule()
        {
            
        }

        public override string ToString() {
            return $"{LengthInHour}, {RegistrationDeadline.ToShortDateString()}, {(Start is null ? "N/A" : Start)} - {(End is null ? "N/A" : End)}";
        }
    }
}