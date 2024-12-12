using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tanfolyam.Helpers.Interfaces;
using Tanfolyam.Models;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IPresentationDataProvider _dataProvider;

        public HomeController(ILogger<HomeController> logger, IRepository repository, UserManager<User> userManager, IPresentationDataProvider dataProvider)
        {
            _logger = logger;
            _repository = repository;
            _userManager = userManager;
            _dataProvider = dataProvider;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                ViewBag.UserBudget = user.Budget;
            }
            var courses = await _dataProvider.BuildPresentationData(user);
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
