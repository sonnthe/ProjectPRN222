using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Controllers
{
    public class SubjectListController(ISubjectService subjectService, IPackageService packageService,
        ILessonService lessonService) : Controller
    {
        private readonly ISubjectService _subjectService = subjectService;
        private readonly IPackageService _packageService = packageService;
        private readonly ILessonService _lessonService = lessonService;

        public IActionResult Index()
        {
            var subjects = _subjectService.GetAllSubjects();
            return View(subjects);
        }

        public IActionResult Detail(int subject_id)
        {

            var subject = _subjectService.GetSubjectById(subject_id);
            ViewBag.Subject = subject;

            var lessonTopics = _lessonService.GetLessonTopicsBySubjectId(subject_id);
            ViewBag.LessonTopics = lessonTopics;

            var packages = _packageService.GetAllPackagesBySubjectId(subject_id);
            ViewBag.Packages = packages;
            return View();
        }
    }
}
