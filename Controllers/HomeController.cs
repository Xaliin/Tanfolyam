using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tanfolyam.Models;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository repository;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IRepository _repository, UserManager<User> userManager)
        {
            this._logger = logger;
            this.repository = _repository;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserBudget = user.Budget;
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

        public async Task<string> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id;
        }
    }
}
