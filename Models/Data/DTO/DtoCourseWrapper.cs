using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Enums;

namespace Tanfolyam.Models.Data.DTO
{
    public class DtoCourseWrapper
    {
        public Course Course { get; set; }
        public Schedule Schedule { get; set; }

        public double UserBalance { get; set; }
        public bool IsLaunchable {  get; set; }
        public bool IsCurrentUserEnrolled { get; set; }
        public bool IsFull {  get; set; }
        public bool DisableFormControll => DetermineIfEnrollButtonShouldBeDisabled();

        public DtoCourseWrapper(Course course, double userBalance, bool isLaunchable, bool isCurrentUserEnrolled, bool isFull)
        {
            Course = course;
            UserBalance = userBalance;
            Schedule = course.Schedule;
            IsLaunchable = isLaunchable;
            IsCurrentUserEnrolled = isCurrentUserEnrolled;
            IsFull = isFull;
        }

        public override string ToString()
        {
            return $"Name: {Course.Name}\n" +
               $"Type: {Course.Type}\n" +
               $"Teacher: {Course.Teacher}\n" +
               $"Description {Course.Description}\n" +
               $"Space: {Course.StudentCount} / {TanfolyamConstants.HeadcountMaximum}\n" +
               $"Status: {Course.Status}";
        }

        private bool DetermineIfEnrollButtonShouldBeDisabled()
        {
            if (Course.Schedule.RegistrationDeadline < DateTime.Now)
            {
                return true;
            }

            if (Course.Status == Status.Active)
            {
                return true;
            }

            if (IsCurrentUserEnrolled)
            {
                return false;
            }

            if (UserBalance < Course.Price)
            {
                return true;
            }

            return IsFull;
        }
    }
}
