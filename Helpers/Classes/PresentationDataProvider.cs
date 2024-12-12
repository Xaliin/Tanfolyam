using Microsoft.AspNetCore.Identity;
using Tanfolyam.Helpers.Interfaces;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.DTO;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Helpers.Classes
{
    public class PresentationDataProvider : IPresentationDataProvider
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository _repository;
        public PresentationDataProvider(UserManager<User> userManager, IRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }


        public async Task<IEnumerable<DtoCourseWrapper>> BuildPresentationDataForCurrentUser(User user)
        {
            var enrollments = await _repository.GetAllEnrollments();
            var courses = await _repository.GetAllCourses();

            var usersCourses= courses.Where(x => enrollments.Where(x => x.UserId == user.Id).ToList().Select(x => x.CourseId).Contains(x.Id)).ToList();

            return await BuildData(user, usersCourses);
        }

        public async Task<IEnumerable<DtoCourseWrapper>> BuildPresentationData(User user)
        {
            var courses = await _repository.GetAllCourses();
            return await BuildData(user, courses);
        }

        public IDictionary<string, int> GetChartDataForAmoutOfStudentsPerTeacher(IEnumerable<Course> courses)
        {
            IDictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var course in courses)
            {
                if (dict.ContainsKey(course.Teacher.Name))
                {
                    dict[course.Teacher.Name] += course.StudentCount;
                }
                else dict.Add(course.Teacher.Name, course.StudentCount);
            }

            return dict;
        }
        public IDictionary<string, double> GetChartDataForIncomeFromEnrollments(IEnumerable<Enrollment> enrollments)
        {
            IDictionary<string, double> dict = new Dictionary<string, double>();
            foreach (var enrollment in enrollments) 
            {
                if (dict.ContainsKey(enrollment.Course.Name))
                {
                    dict[enrollment.Course.Name] += enrollment.AmountToBePayed;
                }
                else dict.Add(enrollment.Course.Name, enrollment.AmountToBePayed);
            }

            return dict;
        }

        private async Task<IEnumerable<DtoCourseWrapper>> BuildData(User user, IEnumerable<Course> courses)
        {
            var dtoCourses = new List<DtoCourseWrapper>();

            foreach (var course in courses)
            {
                bool isLaunchable = DetermineIfCourseISLaunchable(course);
                bool isCurrentUserEnrolled = await DetermineIfCurrentUserIsEnrolled(user, course);
                bool isFull = DetermineIfCourseIsFull(course);
                var newDtoCourse = new DtoCourseWrapper(course, user.Budget, isLaunchable, isCurrentUserEnrolled, isFull);
                dtoCourses.Add(newDtoCourse);
            }

            return dtoCourses;
        }

        private bool DetermineIfCourseIsFull(Course course)
        {
            return course.StudentCount >= TanfolyamConstants.HeadcountMaximum;
        }

        private async Task<bool> DetermineIfCurrentUserIsEnrolled(User user, Course course)
        {
            if (user is null) return false;
            var enrollments = await _repository.GetAllEnrollments();
            return enrollments.Any(x => x.UserId == user.Id && x.CourseId == course.Id);
        }

        private bool DetermineIfCourseISLaunchable(Course course)
        {
            return course.StudentCount >= TanfolyamConstants.HeadcountMinimum &&
                course.StudentCount < TanfolyamConstants.HeadcountMaximum; 
        }

    }
}
