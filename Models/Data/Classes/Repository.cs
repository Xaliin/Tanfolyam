using Microsoft.EntityFrameworkCore;
using Tanfolyam.Data;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Models.Data.Classes
{
    public class Repository : IRepository
    {
        private readonly CourseContext _courseContext;
        public Repository(CourseContext courseContext)
        {
            this._courseContext = courseContext;
        }
        public async Task AddCourse(ICourse course)
        {
            _courseContext.Courses.Add((Course)course);
            await _courseContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ICourse>> GetAllCourses()
        {
            return _courseContext.Courses.Include(c => c.Headcount).ThenInclude(h => h.Students).Include(c => c.Schedule).ToList();
        }
        public async Task<IEnumerable<ITeacher>> GetAllTeachers()
        {
            return _courseContext.Teachers.ToList();
        }

        public async Task AddTeacher(ITeacher teacher)
        {
            _courseContext.Teachers.Add((Teacher)teacher);
            await _courseContext.SaveChangesAsync();
        }

        public async Task<ITeacher> GetTeacherById(int id)
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

    }
}
