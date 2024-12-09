using Tanfolyam.Models.Data.Classes;

namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IRepository
    {
        public Task AddCourse(ICourse course);
        public Task<IEnumerable<ICourse>> GetAllCourses();
        public Task AddTeacher(ITeacher teacher);
    }
}
