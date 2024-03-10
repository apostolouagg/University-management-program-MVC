using Ergasia2mvc.Data;
using Ergasia2mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ergasia2mvc.Controllers
{

    public class UserController : Controller
    {
        private readonly MvcDbContext _context;

        public UserController(MvcDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModelrequest) 
        {
            List<User> users = new List<User>();
            List<Student> students = new List<Student>();
            List<Professor> professors = new List<Professor>();
            List<Secretary> secretaries = new List<Secretary>();

            users = await _context.Users.ToListAsync();
            students = await _context.Students.ToListAsync();
            professors = await _context.Professors.ToListAsync();
            secretaries = await _context.Secretaries.ToListAsync();

            ViewBag.flag = false;

            foreach (var data in users)
            {

                if (data.Username.Equals(loginViewModelrequest.Username) &&
                    data.Password.Equals(loginViewModelrequest.Password) &&
                    data.Role.Equals(loginViewModelrequest.Role))
                {

                    if (data.Role == "Student")
                    {
                        foreach (Student student in students)
                        {
                            if (student.StudentUsername.Equals(data.Username))
                            {
                                return RedirectToAction("StudentIndex", "Student", student);
                            }
                        }
                    }
                    else if (data.Role == "Professor")
                    {
                        foreach (Professor professor in professors)
                        {
                            if (professor.ProfessorUsername.Equals(data.Username))
                            {
                                return RedirectToAction("ProfessorIndex", "Professor", professor);
                            }
                        }
                    }
                    else if (data.Role == "Secretary")
                    {
                        foreach (Secretary secretary in secretaries)
                        {
                            if (secretary.SecretaryUsername.Equals(data.Username))
                            {
                                return RedirectToAction("SecretaryIndex", "Secretary", secretary);
                            }
                        }
                    }

                }
                else
                {
                    ViewBag.flag = true;
                    ViewBag.AlertMessage = "Username, Password or Role is incorrect! Please try again.";
                }
            }

            return View();
        }
    }
}
