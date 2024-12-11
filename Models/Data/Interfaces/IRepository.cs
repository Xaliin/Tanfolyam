using Tanfolyam.Models.Data.Classes;

namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IRepository
    {
        public Task AddCourse(Course course);
        public Task AddTeacher(Teacher teacher);
        public Task AddBudget(double budget, string userId);
        public Task<IEnumerable<Course>> GetAllCourses();
        public Task<IEnumerable<Teacher>> GetAllTeachers();
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<Teacher> GetTeacherById(int id);
        public Task<Course> GetCourseById(int id);
        public Task<User> GetUserById(string id);
        public Task UpdateTeacher(int id, string name);
        public Task DeleteTeacher(int id);
        public Task UpdateCourse(int id, string name, int teacherId, string type, string description, double price, double lengthInHour, DateTime deadline);
        public Task DeleteCourse(int id);
    }
}
