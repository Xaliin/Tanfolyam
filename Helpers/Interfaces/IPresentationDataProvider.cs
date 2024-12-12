using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.DTO;

namespace Tanfolyam.Helpers.Interfaces
{
    public interface IPresentationDataProvider
    {
        public Task<IEnumerable<DtoCourseWrapper>> BuildPresentationDataForCurrentUser(User user);

        public Task<IEnumerable<DtoCourseWrapper>> BuildPresentationData(User user);
    }
}
