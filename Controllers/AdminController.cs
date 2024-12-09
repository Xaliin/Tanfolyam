using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using Tanfolyam.Models.Data.Classes;
using Tanfolyam.Models.Data.Enums;
using Tanfolyam.Models.Data.Interfaces;

namespace Tanfolyam.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository repository;

        public AdminController(IRepository _repository)
        {
            this.repository = _repository;
        }

       

        [HttpPost]
        public async Task<IActionResult> AddCourse(string name, CourseType type, ITeacher teacher, string description, double price, double lengthInHour, DateTime registrationDeadline)
        {
            var schedule = new Schedule(lengthInHour, registrationDeadline);
            var course = new Course(name, type, teacher, description, price, schedule);

            await repository.AddCourse(course);
            return View("Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(string name)
        {
            var teacher = new Teacher(name);
            await repository.AddTeacher(teacher);
            return View("Admin");
        }
    }
}
