using Microsoft.AspNetCore.Mvc;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;

        public CourseController(ILogger<HomeController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _repository.GetAllCourses();
            return View("CourseIndex", courses);
        }
    }
}
