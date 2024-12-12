using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tanfolyam.Helpers.Interfaces;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Enums;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly IPresentationDataProvider _presentationDataProvider;

        public AdminController(ILogger<HomeController> logger, IRepository repository, UserManager<User> userManager, IPresentationDataProvider presentationDataProvider)
        {
            _logger = logger;
            _repository = repository;
            _userManager = userManager;
            _presentationDataProvider = presentationDataProvider;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _repository.GetAllUsers();
            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.UserName
            }).ToList();
            return View("AdminIndex");
        }

        public async Task<IActionResult> CourseListing()
        {
            var user = await _userManager.GetUserAsync(User);
            var courses = await _presentationDataProvider.BuildPresentationData(user);
            return View("CourseListing", courses);
        }

        public async Task<IActionResult> TeacherListing()
        {
            var teachers = await _repository.GetAllTeachers();
            return View("TeacherListing", teachers);
        }

        public async Task<IActionResult> OpenAddTeacherView()
        {
            var teacher = new Teacher();
            return View("AddTeacher", teacher);
        }

        public async Task<IActionResult> OpenTeacherEditView(int id)
        {
            var teacher = await _repository.GetTeacherById(id);
            return View("TeacherEdit", teacher);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(string name)
        {
            var teacher = new Teacher(name);
            await _repository.AddTeacher(teacher);
            return RedirectToAction("TeacherListing");
        }

        public async Task<IActionResult> TeacherEdit(int id, string name)
        {
            await _repository.UpdateTeacher(id, name);
            return RedirectToAction("TeacherListing");
        }


        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _repository.DeleteTeacher(id);
            return RedirectToAction("TeacherListing");
        }

        public async Task<IActionResult> OpenAddCourseView()
        {
            var teachers = await _repository.GetAllTeachers();
            var types = Enum.GetValues(typeof(CourseType))
                          .Cast<CourseType>()
                          .Select(e => new SelectListItem
                          {
                              Value = e.ToString(),
                              Text = e.ToString()
                          }).ToList();
            var course = new Course();
            ViewBag.Teachers = teachers.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            ViewBag.courseTypes = types;
            return View("AddCourse", course);
        }

        public async Task<IActionResult> OpenCourseEditView(int courseId)
        {
            var course = await _repository.GetCourseById(courseId);
            var teachers = await _repository.GetAllTeachers();
            var types = Enum.GetValues(typeof(CourseType))
                          .Cast<CourseType>()
                          .Select(e => new SelectListItem
                          {
                              Value = e.ToString(),
                              Text = e.ToString()
                          }).ToList();
            ViewBag.courseTypes = types;
            ViewBag.Teachers = teachers.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            return View("CourseEdit", course);
        }
        public async Task<IActionResult> CourseEdit(int courseId, int studentCount, string name, int teacherId, string type, string description, double price, double lengthInHour, DateTime deadline)
        {
            var teacher = await _repository.GetTeacherById(teacherId);
            var convertedType = (CourseType)Enum.Parse(typeof(CourseType), type);
            var schedule = new Schedule(lengthInHour, deadline);

            var course = new Course(name, convertedType, teacher, description, price, schedule)
            {
                Id = courseId,
                StudentCount = studentCount
                
            };
            await _repository.UpdateCourse(course);
            return RedirectToAction("CourseListing");
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(string name, int teacher, string type, string description, double price, double lengthInHour, DateTime deadline)
        {
            var rteacher = await _repository.GetTeacherById(teacher);
            var schedule = new Schedule(lengthInHour, deadline);
            var courseType = (CourseType)Enum.Parse(typeof(CourseType), type);

            var course = new Course(name, courseType, rteacher, description, price, schedule);

            await _repository.AddCourse(course);
            return RedirectToAction("CourseListing");
        }

        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            await _repository.DeleteCourse(courseId);
            return RedirectToAction("CourseListing");
        }

        [HttpPost]
        public async Task<IActionResult> AddBudget(double budget, string userId)
        {
            await _repository.AddBudget(budget, userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> OpenActivateCourse(int courseId)
        {
            var course = await _repository.GetCourseById(courseId);
            var schedule = course.Schedule;
            ViewBag.CourseId = courseId;
            return View("ActivateCourse", schedule);
        }

        public async Task<IActionResult> ActivateCourse(int courseId, int scheduleId, DateTime start, DateTime end)
        {
            var course = await _repository.GetCourseById(courseId);
            course.Status = Status.Active;
            await _repository.UpdateCourse(course);
            var schedule = course.Schedule;
            schedule.Start = start;
            schedule.End = end;
            await _repository.UpdateSchedule(schedule);

            return RedirectToAction("CourseListing");
        }

        public async Task<IActionResult> DeactivateCourse(int courseId)
        {
            var course = await _repository.GetCourseById(courseId);
            course.Status = Status.Inactive;
            await _repository.UpdateCourse(course);
            var schedule = course.Schedule;
            schedule.Start = null;
            schedule.End = null;
            await _repository.UpdateSchedule(schedule);

            return RedirectToAction("CourseListing");
        }

        public async Task<IActionResult> GetChartDataForAmoutOfStudentsPerTeacher(DateTime startDate, DateTime endDate)
        {
            var courses = await _repository.GetAllCourses();
            courses = FilterCouresByDate(courses, startDate, endDate);
            var data = _presentationDataProvider.GetChartDataForAmoutOfStudentsPerTeacher(courses);

            return Json(data);
        }

        private IEnumerable<Course> FilterCouresByDate(IEnumerable<Course> courses, DateTime start, DateTime end)
        {
            var filteredList = new List<Course>();
            foreach (var course in courses)
            {
                if (course.Schedule.Start is not null && course.Schedule.Start >= start &&
                    course.Schedule.Start <= end)
                {
                    filteredList.Add(course);
                }
            }

            return filteredList;
        }
    }
}
