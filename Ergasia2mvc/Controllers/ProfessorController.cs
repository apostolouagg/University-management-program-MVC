using Ergasia2mvc.Data;
using Ergasia2mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ergasia2mvc.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly MvcDbContext _context;

        public ProfessorController(MvcDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ProfessorIndex(Professor professor)
        {
            ViewBag.Professor = professor.ProfessorUsername;

            List<Student> students = new List<Student>();
            students = await _context.Students.ToListAsync();
            ViewBag.StudentsList = students;

            List<Course> courseList = new List<Course>();
            courseList = await _context.Courses.ToListAsync();
            ViewBag.courseList = courseList;

            List<CourseHasStudents> courseHasStudents = new List<CourseHasStudents>();
            courseHasStudents = await _context.CourseHasStudents.ToListAsync();
            ViewBag.courseHasStudents = courseHasStudents;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProfessorGradeIndex(CourseHasStudents c)
        {
            ViewBag.StudentID = c.StudentID;

            List<Course> courseList = new List<Course>();
            courseList = await _context.Courses.ToListAsync();
            ViewBag.courseList = courseList;

            int Id = c.CourseID;
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == Id);

            if (course != null)
            {
                var currentCourse = new GradeCourseViewModel()
                {
                    CourseId = c.CourseID,
                    CourseTitle = course.CourseTitle,
                    CourseSemester = course.CourseSemester,
                    Grade = c.GradeCourseStudent,
                    profName = course.ProfessorAFM
                };

                ViewBag.profName = course.ProfessorAFM;

                return View(currentCourse);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProfessorGradeIndex(GradeCourseViewModel gradeCourseViewModel)
        {
            var course = await _context.CourseHasStudents.FindAsync(gradeCourseViewModel.CourseId, gradeCourseViewModel.StudentId);

            if (course != null) 
            {
                course.GradeCourseStudent = gradeCourseViewModel.Grade;
                await _context.SaveChangesAsync();

                Professor professor = new Professor();
                professor.ProfessorUsername = gradeCourseViewModel.profName;

                return RedirectToAction("ProfessorIndex", "Professor", professor);
            }

            return View();
        }
    }
}
