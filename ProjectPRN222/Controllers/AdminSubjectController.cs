using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;
using System.Diagnostics;

namespace ProjectPRN222.Controllers
{
    public class AdminSubjectController(ISubjectService subjectService, ICategoryService categoryService, 
        IAccountService accountService, ILessonService lessonService, IPackageService packageService) : Controller
    {
        private readonly ISubjectService _subjectService = subjectService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IAccountService _accountService = accountService;
        private readonly ILessonService _lessonService = lessonService;
        private readonly IPackageService _packageService = packageService;
        [BindProperty]
        public Subject? Subject { get; set; }
        [BindProperty]
        public LessonTopic? LessonTopic { get; set; }
        public IActionResult Index()
        {
            List<Subject> subjects = _subjectService.GetAllSubjectsForAdmin();
            return View(subjects);
        }

        public IActionResult AddSubject()
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = categories;

            var admins = _accountService.GetAllAdmin();
            ViewBag.Admins = admins;
            return View();
        }

        public IActionResult DoAdd()
        {
            Subject!.CreatedDate = DateTime.Now;
            Subject!.Status = false;

            _subjectService.AddSubject(Subject);
            return RedirectToAction("Index");
        }

    }
}
