using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using SQLitePCL;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Enums;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;

        public AdminController(ILogger<HomeController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            return View("AdminIndex");
        }

        public async Task<IActionResult> CourseListing()
        {
            var courses = await _repository.GetAllCourses();
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

        [HttpPost]
        public async Task<IActionResult> AddCourse(string name, int teacherId, string type, string description, double price, double lengthInHour, DateTime deadline)
        {
            var teacher = await _repository.GetTeacherById(teacherId);
            var schedule = new Schedule(lengthInHour, deadline);
            var courseType = (CourseType)Enum.Parse(typeof(CourseType), type);

            var course = new Course(name, courseType, teacher, description, price, schedule);
            
            await _repository.AddCourse(course);
            return RedirectToAction("CourseListing");
        }
    }
}
