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
            return _courseContext.Courses.Include(c => c.Headcount).ThenInclude(c => c.Students).ToList();
        }
        public async Task AddTeacher(ITeacher teacher)
        {
            _courseContext.Teachers.Add((Teacher)teacher);
            await _courseContext.SaveChangesAsync();
        }
    }
}
