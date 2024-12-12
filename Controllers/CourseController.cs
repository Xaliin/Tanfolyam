using Humanizer;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tanfolyam.Helpers.Interfaces;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Interfaces;
using Tanfolyam.Models.Data.DTO;

namespace Tanfolyam.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IPresentationDataProvider _dataProvider;

        public CourseController(ILogger<HomeController> logger, IRepository repository, UserManager<User> userManager, IPresentationDataProvider dataProvider)
        {
            _logger = logger;
            _repository = repository;
            _userManager = userManager;
            _dataProvider = dataProvider;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();
            var dtoCourses = await _dataProvider.BuildPresentationDataForCurrentUser(user);
            return View("CourseIndex", dtoCourses);
        }

        [HttpPost]
        public async Task<IActionResult> AddToMyCourse(int courseId)
        {
            var user = await GetCurrentUser();
            var course = await _repository.GetCourseById(courseId);
            var enrollment = new Enrollment
            {
                Course = course,
                User = user,
                AmountToBePayed = course.Price
            };
            await _repository.AddEnrollment(enrollment);
            course.StudentCount++;
            user.Budget -= course.Price;
            await _repository.UpdateCourse(course);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromMyCourse(int courseId)
        {
            var user = await GetCurrentUser();
            var course = await _repository.GetCourseById(courseId);
            course.StudentCount--;
            var enrollment = await _repository.GetEnrollmentByUserAndCourse(user.Id, courseId);
            user.Budget += enrollment.AmountToBePayed;
            await _repository.UpdateCourse(course);
            await _repository.DeleteEnrollment(user.Id, courseId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ExportToXML(string action)
        {
            var user = await GetCurrentUser();
            IEnumerable<DtoCourseWrapper> courses;
            if (action == "ExportAllToXML")
            {
                courses = await _dataProvider.BuildPresentationData(user);
            }
            else
            {
                courses = await _dataProvider.BuildPresentationDataForCurrentUser(user);
            }
            return await WriteXml(courses);
        }

        private async Task<IActionResult> WriteXml(IEnumerable<DtoCourseWrapper> courses)
        {
            string fileName = "Tanfolyamok.xml";

            var xml = new TanfolyamXml(courses);
            using (var writer = new StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(typeof(TanfolyamXml));
                serializer.Serialize(writer, xml);
            }
            if (!System.IO.File.Exists(fileName))
            {
                return NotFound();
            }

            Stream stream = System.IO.File.OpenRead(fileName);
            return File(stream, "application/xml", fileName);
        }

        private async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }

    }
}
