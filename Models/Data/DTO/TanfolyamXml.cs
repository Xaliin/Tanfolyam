using Tanfolyam.Models.Data.Classes;

namespace Tanfolyam.Models.Data.DTO
{
    public class TanfolyamXml
    {
        public List<CourseXml> Courses { get; set; }

        public TanfolyamXml(IEnumerable<DtoCourseWrapper> courses)
        {
            Courses = new List<CourseXml>();
            foreach (var course in courses) 
            {
                Courses.Add(new CourseXml(course));
            }
        }

        public TanfolyamXml()
        {
                
        }
    }
}
