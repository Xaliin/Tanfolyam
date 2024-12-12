using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.DTO;

namespace Tanfolyam.Helpers.Interfaces
{
    public interface IPresentationDataProvider
    {
        public Task<IEnumerable<DtoCourseWrapper>> BuildPresentationDataForCurrentUser(User user);

        public Task<IEnumerable<DtoCourseWrapper>> BuildPresentationData(User user);

        public IDictionary<string, int> GetChartDataForAmoutOfStudentsPerTeacher(IEnumerable<Course> courses);

        public IDictionary<string, double> GetChartDataForIncomeFromEnrollments(IEnumerable<Enrollment> enrollments);
    }
}
