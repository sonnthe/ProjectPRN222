using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;
using System.Diagnostics;

namespace ProjectPRN222.Controllers
{
    public class HomeController(ISubjectService subjectService,
        IAccountService accountService, ILessonService lessonService) : Controller
    {
        private readonly ISubjectService _subjectService = subjectService;
        private readonly IAccountService _accountService = accountService;
        private readonly ILessonService _lessonService = lessonService;


        [BindProperty]
        public Account Account { get; set; }

        public IActionResult Index()
        {
            var subjects = _subjectService.GetAllSubjects();
            int subjectCount = _subjectService.GetAllSubjectCount();
            ViewBag.TotalSubjects = subjectCount;

            int accountCount = _accountService.GetAllAccountCount();
            ViewBag.TotalUsers = accountCount;

            int lessonCount = _lessonService.GetAllLessonCount();
            ViewBag.TotalLessons = lessonCount;
            return View(subjects);
        }

        public IActionResult Login()
        {
            var account = _accountService.GetAccountByEmailAndPassword(Account.Email, Account.Password);
            if (account != null)
            {
                HttpContext.Session.SetString("Email", account.Email);
                HttpContext.Session.SetString("Role", account.Role.RoleName);
                HttpContext.Session.SetInt32("AccountId", account.AccountId);
                if (account.Role.RoleName == "Admin")
                {
                    return RedirectToAction("Index", "AdminSubject");
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
