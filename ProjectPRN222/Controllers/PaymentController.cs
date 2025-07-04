using Microsoft.AspNetCore.Mvc;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;
using ProjectPRN222.Utils;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace ProjectPRN222.Controllers
{
    public class PaymentController(ISubjectService subjectService, IPackageService packageService, 
        IRegistrationService registrationService) : Controller
    {
        private readonly ISubjectService _subjectService = subjectService;
        private readonly IPackageService _packageService = packageService;
        private readonly IRegistrationService _registrationService = registrationService;

        public IActionResult Index(int courseDuration, int accountId, int subjectId)
        {
            var subject = _subjectService.GetSubjectById(subjectId);
            ViewBag.Subject = subject;

            var package = _packageService.GetPackageById(courseDuration);
            ViewBag.Package = package;

            ViewBag.AccountId = accountId;
            return View();
        }
        [HttpPost]

        public IActionResult RedirectToVnpay(IFormCollection form)
        {
            string vnp_Version = "2.1.0";
            string vnp_Command = "pay";
            int orderType = 8;

            string subject_id = form["subject_id"];
            string account_id = form["account_id"];
            string sale_price = form["sale_price"];
            string package_id = form["package_id"];
            long amount = long.Parse(form["amount"]) * 100;
            string bankCode = form["bankCode"];

            string vnp_TxnRef = Config.GetRandomNumber(8);
            string vnp_IpAddr = Config.GetIpAddress(HttpContext);

            string vnp_TmnCode = Config.vnp_TmnCode;

            var vnp_Params = new Dictionary<string, string>
        {
            { "vnp_Version", vnp_Version },
            { "vnp_Command", vnp_Command },
            { "vnp_TmnCode", vnp_TmnCode },
            { "vnp_Amount", amount.ToString() },
            { "vnp_CurrCode", "VND" },
            { "vnp_TxnRef", vnp_TxnRef },
            { "vnp_OrderInfo", $"{subject_id}|{account_id}|{sale_price}|{package_id}" },
            { "vnp_OrderType", orderType.ToString() },
            { "vnp_ReturnUrl", Config.vnp_ReturnUrl },
            { "vnp_IpAddr", vnp_IpAddr }
        };

            if (!string.IsNullOrEmpty(bankCode))
            {
                vnp_Params.Add("vnp_BankCode", bankCode);
            }

            string locate = form["language"];
            vnp_Params.Add("vnp_Locale", string.IsNullOrEmpty(locate) ? "vn" : locate);

            var cld = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            vnp_Params.Add("vnp_CreateDate", cld.ToString("yyyyMMddHHmmss"));
            vnp_Params.Add("vnp_ExpireDate", cld.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var sortedKeys = vnp_Params.Keys.OrderBy(k => k).ToList();
            var hashData = new StringBuilder();
            var query = new StringBuilder();

            foreach (var key in sortedKeys)
            {
                var value = vnp_Params[key];
                if (!string.IsNullOrEmpty(value))
                {
                    hashData.Append($"{key}={WebUtility.UrlEncode(value)}&");
                    query.Append($"{WebUtility.UrlEncode(key)}={WebUtility.UrlEncode(value)}&");
                }
            }

            if (hashData.Length > 0) hashData.Length -= 1;
            if (query.Length > 0) query.Length -= 1;

            string vnp_SecureHash = Config.HmacSHA512(Config.secretKey, hashData.ToString());
            query.Append("&vnp_SecureHash=").Append(vnp_SecureHash);
            string paymentUrl = Config.vnp_PayUrl + "?" + query;

            return Redirect(paymentUrl);
        }

        public IActionResult PurchaseResult(string vnp_ResponseCode, string vnp_TxnRef, string vnp_TransactionNo,
            string vnp_Amount, string vnp_PayDate, string vnp_OrderInfo)
        {
            ViewBag.ResponseCode = vnp_ResponseCode;
            ViewBag.TxnRef = vnp_TxnRef;
            ViewBag.TransactionNo = vnp_TransactionNo;
            ViewBag.Amount = vnp_Amount;


            DateTime date = DateTime.ParseExact(vnp_PayDate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            string formattedDate = date.ToString("dd-MM-yyyy");
            ViewBag.PayDate = formattedDate;

            if (vnp_ResponseCode == "00")
            {
                int subjectId = int.Parse(vnp_OrderInfo.Split('|')[0]);
                int accountId = int.Parse(vnp_OrderInfo.Split('|')[1]);
                bool isRegistered = _registrationService.IsRegistered(accountId, subjectId);
                if (!isRegistered)
                {
                    bool isSuccess = _registrationService.PurchaseSubject(vnp_OrderInfo);
                    if (isSuccess)
                    {
                        ViewBag.Message = "Payment successful! Your purchase has been confirmed.";
                    }
                    else
                    {
                        ViewBag.Message = "Payment successful, but there was an issue confirming your purchase.";
                    }
                }
                else
                {
                    Debug.WriteLine("Already registered, confirming purchase...");
                    bool isSuccess = _registrationService.ConfirmPurchaseRegisteredSubject(vnp_OrderInfo);
                    if (isSuccess)
                    {
                        ViewBag.Message = "Payment successful! Your purchase has been confirmed.";
                    }
                    else
                    {
                        ViewBag.Message = "Payment successful, but there was an issue confirming your purchase.";
                    }
                }

                    return View();
            }
            else
            {
                return View("Failure");
            }
        }
    }
}
