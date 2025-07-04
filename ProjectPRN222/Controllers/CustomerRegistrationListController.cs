using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Controllers
{

    public class CustomerRegistrationListController(IRegistrationService registrationService) : Controller
    {
        private readonly IRegistrationService _registrationService = registrationService;
        public List<Subject> BoughtSubjectList = [];
        public List<Subject> RegisteredSubjectList = [];
        public IActionResult Index()
        {
            int accountId = HttpContext.Session.GetInt32("AccountId") ?? 0;
            BoughtSubjectList = _registrationService.GetBoughtSubjects(accountId);
            ViewBag.BoughtSubjectList = BoughtSubjectList;

            RegisteredSubjectList = _registrationService.GetRegisteredSubjects(accountId);
            ViewBag.RegisteredSubjectList = RegisteredSubjectList;
            return View();
        }

        public IActionResult Remove(int subject_id)
        {
            int accountId = HttpContext.Session.GetInt32("AccountId") ?? 0;
            _registrationService.RemoveRegistration(accountId, subject_id);
            return RedirectToAction("Index");
        }
    }
}
