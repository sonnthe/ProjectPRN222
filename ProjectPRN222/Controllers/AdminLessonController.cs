using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Impl;
using ProjectPRN222.Services.Interface;
using System.Diagnostics;

namespace ProjectPRN222.Controllers
{
    public class AdminLessonController(ILessonService lessonService, ISubjectService subjectService,
        IPackageService packageService) : Controller
    {
        private readonly ILessonService _lessonService = lessonService;
        private readonly ISubjectService _subjectService = subjectService;
        private readonly IPackageService _packageService = packageService;
        [BindProperty]
        public Lesson? Lesson { get; set; }
        [BindProperty]
        public LessonTopic? LessonTopic { get; set; }
        [BindProperty]
        public Package? Package { get; set; }
        
        //Lesson
        public IActionResult LessonList(int subject_id, int lesson_topic_id)
        {
            List<Lesson> lessons = _lessonService.GetAllLessonsBySubjectId(subject_id, lesson_topic_id);
            ViewBag.SubjectId = subject_id;
            ViewBag.LessonTopicId = lesson_topic_id;
            return View(lessons);
        }

        public IActionResult AddLesson( int lesson_id,int lesson_topic_id, int subject_id)
        {
            ViewBag.SubjectId = subject_id;
            ViewBag.LessonTopicId = lesson_topic_id;
            Lesson = _lessonService.GetLessonById(lesson_id);
            return View(Lesson);
        }

        public IActionResult DoAddLesson()
        {
            _lessonService.AddLesson(Lesson!);
            return RedirectToAction("AddLesson", new { subject_id = Lesson!.SubjectId });
        }
        public IActionResult DoUpdateLesson()
        {
            _lessonService.UpdateLesson(Lesson!);
            return RedirectToAction("LessonList", new { subject_id = Lesson!.SubjectId });
        }
        public IActionResult DoDeleteLesson(int lesson_id, int subject_id)
        {
            _lessonService.DeleteLesson(lesson_id);
            return RedirectToAction("LessonList", new { subject_id });
        }

        //Lesson Topic
        public IActionResult LessonTopicList(int subject_id)
        {
            var lessonTopics = _lessonService.GetLessonTopicsBySubjectId(subject_id);
            ViewBag.SubjectId = subject_id;
            return View(lessonTopics);
        }
        public IActionResult AddLessonTopic(int subject_id, int lesson_topic_id)
        {
            ViewBag.SubjectId = subject_id;
            var lessonTopic = _lessonService.GetLessonTopicById(lesson_topic_id);
            return View(lessonTopic);
        }
        public IActionResult DoAddLessonTopic()
        {
            _lessonService.AddLessonTopic(LessonTopic!);
            return RedirectToAction("AddLessonTopic", new { subject_id = LessonTopic!.SubjectId });
        }
        public IActionResult DoUpdateLessonTopic()
        {
            _lessonService.UpdateLessonTopic(LessonTopic!);
            return RedirectToAction("LessonTopicList", new { subject_id = LessonTopic!.SubjectId });
        }
        public IActionResult DoDeleteLessonTopic(int lesson_topic_id, int subject_id)
        {
            _lessonService.DeleteLessonTopic(lesson_topic_id);
            return RedirectToAction("LessonTopicList", new { subject_id });
        }

        //Package
        public IActionResult PricePackageList(int subject_id)
        {
            List<Package> packages = _packageService.GetAllPackagesBySubjectId(subject_id);
            ViewBag.SubjectId = subject_id;
            return View(packages);
        }
        public IActionResult AddPricePackage(int subject_id, int package_id)
        {
            ViewBag.SubjectId = subject_id;
            Package package = _packageService.GetPackageById(package_id);
            return View(package);
        }
        public IActionResult DoAddPricePackage()
        {
            Package!.Status = true;
            _packageService.AddPackage(Package);
            return RedirectToAction("AddPricePackage", new {subject_id = Package.SubjectId});
        }
        public IActionResult DoUpdatePricePackage()
        {
            _packageService.UpdatePackage(Package!);
            return RedirectToAction("PricePackageList", new {subject_id = Package!.SubjectId});
        }
        public IActionResult DoDeletePricePackage(int package_id, int subject_id)
        {
            _packageService.DeletePackage(package_id);
            return RedirectToAction("PricePackageList", new { subject_id });
        }
    }
}
