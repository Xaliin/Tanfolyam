using Tanfolyam.Models.Data.Classes;

namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IRepository
    {
        public Task AddCourse(ICourse course);
        public Task<IEnumerable<ICourse>> GetAllCourses();
        public Task<IEnumerable<ITeacher>> GetAllTeachers();
        public Task AddTeacher(ITeacher teacher);
        public Task<ITeacher> GetTeacherById(int id);
        public Task UpdateTeacher(int id, string name);
        public Task DeleteTeacher(int id);
    }
}
