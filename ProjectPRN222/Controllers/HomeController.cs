using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;
using System.Diagnostics;

namespace ProjectPRN222.Controllers
{
    public class HomeController(ISubjectService subjectService, IAccountService accountService) : Controller
    {
        private readonly ISubjectService _subjectService = subjectService;
        private readonly IAccountService _accountService = accountService;


        [BindProperty]
        public Account Account { get; set; }

        public IActionResult Index()
        {
            var subjects = _subjectService.GetAllSubjects();
            return View(subjects);
        }

        public IActionResult Login()
        {
            Debug.WriteLine("Admin logged in");
            var account = _accountService.GetAccountByEmailAndPassword(Account.Email, Account.Password);
            if (account != null)
            {
                HttpContext.Session.SetString("Email", account.Email);
                HttpContext.Session.SetString("Role", account.Role.RoleName);
                HttpContext.Session.SetInt32("AccountId", account.AccountId);
                if (account.Role.RoleName == "Admin")
                {
                    return RedirectToAction("Index", "AdminDashBoard");
                }
                return RedirectToAction("Index", "CustomerHome");
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid email or password";    
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            if (ModelState.IsValid)
            {
                _accountService.Register(Account);
                return RedirectToAction("Index");
            }
            return View(Account);
        }
    }

}
