using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tanfolyam.Models;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository repository;

        public HomeController(ILogger<HomeController> logger, IRepository _repository)
        {
            _logger = logger;
            this.repository = _repository;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await repository.GetAllCourses();
            return View("HomeIndex", courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
