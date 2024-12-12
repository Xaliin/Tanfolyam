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
        public async Task AddTeacher(Teacher teacher)
        {
            _courseContext.Teachers.Add((Teacher)teacher);
            await _courseContext.SaveChangesAsync();
        }

        public async Task AddCourse(Course course)
        {
            var result = (Course)course;
            _courseContext.Courses.Add(result);
            await _courseContext.SaveChangesAsync();
        }

        public async Task AddEnrollment(Enrollment enrollment)
        {
            _courseContext.Enrollments.Add(enrollment);
            await _courseContext.SaveChangesAsync();
        }

        public async Task AddBudget(double budget, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.Budget += budget;
            await _courseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return await _courseContext.Teachers.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return _courseContext.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Schedule)
                .ToList();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _courseContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            return await _courseContext.Enrollments.ToListAsync();
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            return await _courseContext.Teachers.FindAsync(id);
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _courseContext.Courses.Include(c => c.Schedule).Include(c => c.Teacher).FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
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

        public async Task UpdateCourse(Course course)
        {
            _courseContext.Courses.Update(course);
            await _courseContext.SaveChangesAsync();
        }

        public async Task DeleteTeacher(int id)
        {
            var result = _courseContext.Teachers.Where(x => x.Id == id).FirstOrDefault();
            _courseContext.Teachers.Remove(result);
            await _courseContext.SaveChangesAsync();
        }

        public async Task DeleteCourse(int id)
        {
            var result = _courseContext.Courses.Where(x => x.Id == id).FirstOrDefault();
            _courseContext.Courses.Remove(result);
            await _courseContext.SaveChangesAsync();
        }

        public async Task DeleteEnrollment(string userId, int courseId)
        {
            var result = _courseContext.Enrollments.Where(x => x.UserId == userId && x.CourseId == courseId).ToList().FirstOrDefault();
            _courseContext.Enrollments.Remove(result);
            await _courseContext.SaveChangesAsync();
        }
    }
}
