using Tanfolyam.Models.Data.Enums;

namespace Tanfolyam.Models.Data.Interfaces
{
    public interface ICourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseType Type { get; set; }
        public ITeacher Teacher { get; }
        public IHeadcount Headcount { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ISchedule Schedule { get; set; }
        public Status Status { get; set; }
    }
}
