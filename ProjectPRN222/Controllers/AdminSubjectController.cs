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

        public IActionResult AddSubject(int subject_id)
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = categories;

            var admins = _accountService.GetAllAdmin();
            ViewBag.Admins = admins;

            var subject = _subjectService.GetSubjectById(subject_id);
            return View(subject);
        }

        public IActionResult DoAdd(IFormFile Thumbnail)
        {
            Subject!.CreatedDate = DateTime.Now;
            Subject!.Status = false;

            if (Thumbnail != null && Thumbnail.Length > 0)
            {
                var fileName = Path.GetFileName(Thumbnail.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Thumbnail.CopyTo(stream); 
                }
                Subject.Thumbnail = "/assets/images/" + fileName;
            }


            _subjectService.AddSubject(Subject);
            return RedirectToAction("Index");
        }

        public IActionResult DoEdit()
        {
            _subjectService.UpdateSubject(Subject!);
            return RedirectToAction("Index");
        }

        public IActionResult DoDelete(int subject_id)
        {
            _subjectService.DeleteSubject(subject_id);
            return RedirectToAction("Index");
        }
    }
}
