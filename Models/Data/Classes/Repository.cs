using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tanfolyam.Data;
using Tanfolyam.Models.Data.Enums;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Models.Data.Classes
{
    public class Repository : IRepository
    {
        private readonly CourseContext _courseContext;
        private readonly UserManager<User> _userManager;
        public Repository(CourseContext courseContext, UserManager<User> userManager)
        {
            this._courseContext = courseContext;
            this._userManager = userManager;
        }
        public async Task AddCourse(Course course)
        {
            var result = (Course)course;
            _courseContext.Courses.Add(result);
            await _courseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return _courseContext.Courses.Include(c => c.Headcount).ThenInclude(h => h.Students).Include(c => c.Schedule).Include(c => c.Teacher).ToList();
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return _courseContext.Teachers.ToList();
        }

        public async Task AddTeacher(Teacher teacher)
        {
            _courseContext.Teachers.Add((Teacher)teacher);
            await _courseContext.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            return await _courseContext.Teachers.FindAsync(id);
        }

        public async Task UpdateTeacher(int id, string name)
        {
            var result = _courseContext.Teachers.Where(x => x.Id == id).FirstOrDefault();
            if (result is not null)
            {
                result.Name = name;
                await _courseContext.SaveChangesAsync();
            }
        }
        public async Task DeleteTeacher(int id)
        {
            var result = _courseContext.Teachers.Where(x => x.Id == id).FirstOrDefault();
            _courseContext.Teachers.Remove(result);
            await _courseContext.SaveChangesAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _courseContext.Courses.Include(c => c.Headcount).ThenInclude(h => h.Students).Include(c => c.Schedule).Include(c => c.Teacher).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCourse(int id, string name, int teacherId, string type, string description, double price, double lengthInHour, DateTime deadline)
        {
            var result = await GetCourseById(id);
            var teacher = await GetTeacherById(teacherId);
            result.Name = name;
            result.Teacher = teacher;
            result.Type = (CourseType)Enum.Parse(typeof(CourseType), type);
            result.Description = description;
            result.Price = price;
            result.Schedule.LengthInHour = lengthInHour;
            result.Schedule.RegistrationDeadline = deadline;
            await _courseContext.SaveChangesAsync();
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            return _courseContext.Schedule.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task DeleteCourse(int id)
        {
            var result = _courseContext.Courses.Where(x => x.Id == id).FirstOrDefault();
            _courseContext.Courses.Remove(result);
            await _courseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _courseContext.Users.Include(u => u.Courses).ToListAsync();
        }

        public async Task AddBudget(double budget, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.Budget += budget;
            await _courseContext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
