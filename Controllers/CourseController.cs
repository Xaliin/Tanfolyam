using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly UserManager<User> _userManager;

        public CourseController(ILogger<HomeController> logger, IRepository repository, UserManager<User> userManager)
        {
            _logger = logger;
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                ViewBag.UserBudget = user.Budget;
            }
            var courses = await _repository.GetUserCourses(user.Id);
            return View("CourseIndex", courses);
        }

        [HttpPost]
        public async Task<IActionResult> AddToMyCourse(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            var course = await _repository.GetCourseById(courseId);
            var headcountId = course.Headcount.Id;
            user.Courses.Add(course);
            await _repository.AddUserToHeadCount(headcountId, user.Id);
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}
