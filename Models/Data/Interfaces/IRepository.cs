using Tanfolyam.Models.Data.Classes;

namespace Tanfolyam.Models.Data.Interfaces
{
    public interface IRepository
    {
        public Task AddTeacher(Teacher teacher);
        public Task AddCourse(Course course);
        public Task AddEnrollment(Enrollment enrollment);
        public Task AddBudget(double budget, string userId);
        public Task<IEnumerable<Teacher>> GetAllTeachers();
        public Task<IEnumerable<Course>> GetAllCourses();
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<IEnumerable<Enrollment>> GetAllEnrollments();
        public Task<Teacher> GetTeacherById(int id);
        public Task<Course> GetCourseById(int id);
        public Task<User> GetUserById(string id);
        public Task<Enrollment> GetEnrollmentByUserAndCourse(string userId, int coruseId);
        public Task UpdateTeacher(int id, string name);
        public Task UpdateCourse(Course course);
        public Task UpdateSchedule(Schedule schedule);
        public Task DeleteTeacher(int id);
        public Task DeleteCourse(int id);
        public Task DeleteEnrollment(string userId, int courseId);
    }
}
