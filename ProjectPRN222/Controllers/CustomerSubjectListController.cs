using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Services.Interface;
using System.Diagnostics;

namespace ProjectPRN222.Controllers
{
    public class CustomerSubjectListController(ISubjectService subjectService, IPackageService packageService,
        ILessonService lessonService, IRegistrationService registrationService) : Controller
    {
        private readonly ISubjectService _subjectService = subjectService;
        private readonly IPackageService _packageService = packageService;
        private readonly ILessonService _lessonService = lessonService;
        private readonly IRegistrationService _registrationService = registrationService;

        public IActionResult Index()
        {
            int accountId = HttpContext.Session.GetInt32("AccountId") ?? 0;
            ViewBag.AccountId = accountId;
            var subjects = _subjectService.GetAllSubjects();
            return View(subjects);
        }

        public IActionResult Detail(int subject_id)
        {
            int accountId = HttpContext.Session.GetInt32("AccountId") ?? 0;
            ViewBag.AccountId = accountId;
            var subject = _subjectService.GetSubjectById(subject_id);
            ViewBag.Subject = subject;

            bool isSubjectRegistered = _registrationService.IsRegistered(accountId, subject_id);
            ViewBag.IsSubjectRegistered = isSubjectRegistered;

            bool isSubjectBought = _registrationService.IsBought(accountId, subject_id);
            ViewBag.IsSubjectBought = isSubjectBought;

            var lessonTopics = _lessonService.GetLessonTopicsBySubjectId(subject_id);
            ViewBag.LessonTopics = lessonTopics;

            var packages = _packageService.GetAllPackagesBySubjectId(subject_id);
            ViewBag.Packages = packages;
            return View();
        }

        public IActionResult Lesson(int lesson_id)
        {
            var lesson = _lessonService.GetLessonById(lesson_id);
            return View(lesson);
        }

        //public IActionResult RegisterSubject(int subjectId, int packageId)
        //{
        //    int accountId = HttpContext.Session.GetInt32("AccountId") ?? 0;
        //    _registrationService.RegisterSubject(accountId, subjectId,packageId);
        //    return RedirectToAction("Index", "CustomerRegistrationList");
        //    return View("Index");
        //}
        public IActionResult RegisterSubject(int subjectId, int packageId)
        {
            int accountId = HttpContext.Session.GetInt32("AccountId") ?? 0;
            _registrationService.RegisterSubject(accountId, subjectId, packageId);

            ViewBag.AccountId = accountId;
            var subjects = _subjectService.GetAllSubjects();
            return View("Index", subjects);
        }

    }
}
