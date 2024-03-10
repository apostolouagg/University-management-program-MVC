using Ergasia2mvc.Data;
using Ergasia2mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ergasia2mvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly MvcDbContext _context;

        public StudentController(MvcDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> StudentIndex(Student student)
        {
            ViewBag.Student = student.StudentUsername;
            ViewBag.StudentLoggedInId = student.RegistrationNumber;

            List<CourseHasStudents> courseHasStudents = new List<CourseHasStudents>();
            courseHasStudents = await _context.CourseHasStudents.ToListAsync();
            ViewBag.courseHasStudents = courseHasStudents;

            List<Course> courseList = new List<Course>();
            courseList = await _context.Courses.ToListAsync();
            ViewBag.courseList = courseList;

            return View(student);
        }
    }
}
