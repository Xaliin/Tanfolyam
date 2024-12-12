using System.Reflection.Metadata.Ecma335;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Enums;

namespace Tanfolyam.Models.Data.DTO
{
    public class CourseXml
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseType Type { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public int StudentCount { get; set; }
        public Status Status { get; set; }
        public double LengthInHour { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }


        public CourseXml(DtoCourseWrapper wrapper)
        {
            Id = wrapper.Course.Id;
            Name = wrapper.Course.Name;
            Type = wrapper.Course.Type;
            Description = wrapper.Course.Description;
            Teacher = wrapper.Course.Teacher.Name;
            StudentCount = wrapper.Course.StudentCount;
            Status = wrapper.Course.Status;
            LengthInHour = wrapper.Schedule.LengthInHour;
            RegistrationDeadline = wrapper.Schedule.RegistrationDeadline;
            Start = wrapper.Schedule.Start;
            End = wrapper.Schedule.End;
        }

        public CourseXml()
        {
            
        }

    }
}
