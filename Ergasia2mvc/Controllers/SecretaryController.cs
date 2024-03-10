using Ergasia2mvc.Data;
using Ergasia2mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ergasia2mvc.Controllers
{
    public class SecretaryController : Controller
    {
        private readonly MvcDbContext _context;

        public SecretaryController(MvcDbContext context)
        {
            this._context = context;
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "User");
        }

        public async Task<IActionResult> SecretaryIndex(Secretary secretary)
        {
            ViewBag.Secretary = secretary.SecretaryUsername;

            List<Course> courses = new List<Course>();
            courses = await _context.Courses.ToListAsync();

            List<AddCourseViewModel> courses2 = new List<AddCourseViewModel>();

            foreach (Course x in courses)
            {
                var model = new AddCourseViewModel()
                {
                    Id = x.CourseId,
                    CourseTitle = x.CourseTitle,
                    CourseSemester = x.CourseSemester,
                    ProfessorAFM = x.ProfessorAFM,
                    secName = secretary.SecretaryUsername,
                };

                courses2.Add(model);
            }

            ViewBag.Course = courses2;

            return View();
        }

        public IActionResult Register(LoginViewModel addWho)
        {
            return RedirectToAction("AddIndex", "Add", addWho);
        }

        [HttpGet]
        public async Task<IActionResult> ViewCourse(AddCourseViewModel course)
        {
            List<Professor> professors = new List<Professor>();
            professors = await _context.Professors.ToListAsync();
            ViewBag.Professors = professors;

            int Id = course.Id;
            var c = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == Id);

            if (c != null) 
            {
                var viewSelectedCourse = new AssignProfessorViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    CourseSemester = c.CourseSemester,
                    ProfessorAFM = c.ProfessorAFM,
                };

                ViewBag.assignedProf = c.ProfessorAFM;
                ViewBag.Secname = course.secName;

                return View(viewSelectedCourse);
            }

            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> ViewCourse(AssignProfessorViewModel assignProfessorViewModel)
        {
            var course = await _context.Courses.FindAsync(assignProfessorViewModel.CourseId);

            if (course != null)
            {
                course.ProfessorAFM = assignProfessorViewModel.ProfessorAFM;

                await _context.SaveChangesAsync();

                Secretary sec = new Secretary();
                sec.SecretaryUsername = assignProfessorViewModel.secName;

                return RedirectToAction("SecretaryIndex", "Secretary", sec);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StudentReport(AddCourseViewModel course)
        {
            ViewBag.CourseName = course.CourseTitle;

            List<Student> students = new List<Student>();
            students = await _context.Students.ToListAsync();
            ViewBag.Students = students;

            List<CourseHasStudents> coursehasstudentslist = new List<CourseHasStudents>();
            coursehasstudentslist = await _context.CourseHasStudents.ToListAsync();

            List<string> studentIDs = new List<string>();
            foreach (CourseHasStudents c in coursehasstudentslist)
            {
                if (c.CourseID.Equals(course.Id))
                {
                    studentIDs.Add(c.StudentID);
                }
            }

            ViewBag.StudentIDs = studentIDs;

            ViewBag.SelectedCourseId = course.Id;
            ViewBag.Secname = course.secName;

            var viewSelectedCourse = new AddToStudentReportViewModel()
            {
                CourseId = course.Id,
                secName = course.secName
            };

            return View(viewSelectedCourse);
        }

        [HttpPost]
        public async Task<IActionResult> StudentReport(AddToStudentReportViewModel addToStudentReportViewModel)
        {
            var coursehasstudents = new CourseHasStudents()
            {
                CourseID = addToStudentReportViewModel.CourseId,
                StudentID = addToStudentReportViewModel.StudentId,
                GradeCourseStudent = "-"
            };

            _context.CourseHasStudents.Add(coursehasstudents);
            await _context.SaveChangesAsync();

            Secretary sec = new Secretary();
            sec.SecretaryUsername = addToStudentReportViewModel.secName;

            return RedirectToAction("SecretaryIndex", "Secretary", sec);
        }
    }
}
