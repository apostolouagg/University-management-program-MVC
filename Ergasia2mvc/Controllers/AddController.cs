using Ergasia2mvc.Data;
using Ergasia2mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ergasia2mvc.Controllers
{
    public class AddController : Controller
    {
        private readonly MvcDbContext _context;

        public AddController(MvcDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AddIndex(User person)
        {
            if (person.Role.Equals("Student"))
            {
                ViewBag.Secname = person.Username;
                return View("~/Views/Add/AddStudentIndex.cshtml");
            }
            else if (person.Role.Equals("Professor"))
            {
                ViewBag.Secname = person.Username;
                return View("~/Views/Add/AddProfessorIndex.cshtml");
            }
            else if (person.Role.Equals("Course"))
            {
                List<Professor> professors= new List<Professor>();
                professors = await _context.Professors.ToListAsync();
                ViewBag.Professors = professors;

                ViewBag.Secname = person.Username;
                return View("~/Views/Add/AddCourseIndex.cshtml");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentViewModel addStudentViewModel)
        {
            try
            {
                ViewBag.flag = false;

                bool regNum = addStudentViewModel.RegistrationNumber.All(Char.IsLetterOrDigit);
                bool name = addStudentViewModel.Name.All(Char.IsLetter);
                bool surname = addStudentViewModel.Surname.All(Char.IsLetter);
                bool department = addStudentViewModel.Department.All(Char.IsLetter);
                bool username = addStudentViewModel.StudentUsername.All(Char.IsLetterOrDigit);

                if (addStudentViewModel.RegistrationNumber.Length == 6 &&
                    regNum && name && surname && department && username)
                {
                    Secretary sec = new Secretary();
                    sec.SecretaryUsername = addStudentViewModel.secName;

                    var student = new Student()
                    {
                        RegistrationNumber = addStudentViewModel.RegistrationNumber,
                        Name = addStudentViewModel.Name,
                        Surname = addStudentViewModel.Surname,
                        Department = addStudentViewModel.Department.ToUpper(),
                        StudentUsername = addStudentViewModel.StudentUsername,
                    };

                    var user = new User()
                    {
                        Username = addStudentViewModel.StudentUsername,
                        Password = addStudentViewModel.Password,
                        Role = "Student"
                    };

                    _context.Students.Add(student);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("SecretaryIndex", "Secretary", sec);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                ViewBag.Secname = addStudentViewModel.secName;
                ViewBag.flag = true;
                ViewBag.AlertMessage = "Something is incorrect or Registration Number and/or Username already exist! Please check your inputs and try again.";
                return View("~/Views/Add/AddStudentIndex.cshtml");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddProfessor(AddProfessorViewModel addProfessorViewModel)
        {
            try
            {
                ViewBag.flag = false;

                bool afm = addProfessorViewModel.AFM.All(Char.IsDigit);
                bool name = addProfessorViewModel.Name.All(Char.IsLetter);
                bool surname = addProfessorViewModel.Surname.All(Char.IsLetter);
                bool department = addProfessorViewModel.Department.All(Char.IsLetter);
                bool username = addProfessorViewModel.ProfessorUsername.All(Char.IsLetterOrDigit);

                if (addProfessorViewModel.AFM.Length == 10 &&
                    afm && name && surname && department && username)
                {
                    Secretary sec = new Secretary();
                    sec.SecretaryUsername = addProfessorViewModel.secName;

                    var professor = new Professor()
                    {
                        AFM = addProfessorViewModel.AFM,
                        Name = addProfessorViewModel.Name,
                        Surname = addProfessorViewModel.Surname,
                        Department = addProfessorViewModel.Department.ToUpper(),
                        ProfessorUsername = addProfessorViewModel.ProfessorUsername,
                    };

                    var user = new User()
                    {
                        Username = addProfessorViewModel.ProfessorUsername,
                        Password = addProfessorViewModel.Password,
                        Role = "Professor"
                    };

                    _context.Professors.Add(professor);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("SecretaryIndex", "Secretary", sec);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                ViewBag.Secname = addProfessorViewModel.secName;
                ViewBag.flag = true;
                ViewBag.AlertMessage = "Something is incorrect or AFM and/or Username already exist! Please check your inputs and try again.";
                return View("~/Views/Add/AddProfessorIndex.cshtml");
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseViewModel addCourseViewModel)
        {
            try
            {
                List<Course> list = new List<Course>();
                list = await _context.Courses.ToListAsync();

                foreach (Course co in list)
                {
                    if (co.CourseTitle.Equals(addCourseViewModel.CourseTitle) || co.ProfessorAFM.IsNullOrEmpty())
                    {
                        throw new Exception();
                    }
                }

                var course = new Course()
                {
                    CourseTitle = addCourseViewModel.CourseTitle,
                    CourseSemester = addCourseViewModel.CourseSemester,
                    ProfessorAFM = addCourseViewModel.ProfessorAFM
                };

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                Secretary sec = new Secretary();
                sec.SecretaryUsername = addCourseViewModel.secName;

                return RedirectToAction("SecretaryIndex", "Secretary", sec);
            }
            catch (Exception e) 
            {
                List<Professor> professors = new List<Professor>();
                professors = await _context.Professors.ToListAsync();
                ViewBag.Professors = professors;

                ViewBag.Secname = addCourseViewModel.secName;
                ViewBag.flag = true;
                ViewBag.AlertMessage = "This Course Title already exists or no Professor was assigned! Please try again.";
                return View("~/Views/Add/AddCourseIndex.cshtml");
            }
            
        }

    }
}
