using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Controllers
{
    public class AdminManageAccountController(IAccountService accountService) : Controller
    {
        private readonly IAccountService _accountService = accountService;

        public IActionResult Index()
        {
            var accounts = _accountService.GetAllAccounts();
            return View(accounts);
        }
    }
}
