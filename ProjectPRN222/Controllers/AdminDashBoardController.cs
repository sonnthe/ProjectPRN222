using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Controllers
{
    public class AdminDashBoardController(ISubjectService subjectService) : Controller
    {
        private readonly ISubjectService _subjectService = subjectService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public IActionResult SubjectList()
        {
            //List<Subject> subjects = _subjectService.GetAllSubjects();
            return View();
        }
    }
}
