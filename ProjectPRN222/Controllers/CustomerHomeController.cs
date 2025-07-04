using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Controllers
{
    public class CustomerHomeController(ISubjectService subjectService, IRegistrationService registrationService) : Controller
    {
        private readonly ISubjectService _subjectService = subjectService;
        private readonly IRegistrationService _registrationService = registrationService;

        public IActionResult Index()
        {
            int accountId= HttpContext.Session.GetInt32("AccountId") ?? 0;
            var boughtSubjectList = _registrationService.GetBoughtSubjects(accountId);
            ViewBag.BoughtSubjectList = boughtSubjectList;
            var subjectList = _subjectService.GetAllSubjects();
            ViewBag.SubjectList = subjectList;
            var keyword = Request.Query["keyword"].ToString();
            ViewBag.Keyword = keyword;
            return View();

        }
    }
}
