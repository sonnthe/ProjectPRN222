using System.Security.Cryptography;
using System.Text;

namespace ProjectPRN222.Utils
{
    public static class Config
    {
        public static string vnp_PayUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public static string vnp_ReturnUrl = "http://localhost:5230/Payment/PurchaseResult";
        public static string vnp_TmnCode = "KTUS6B23";
        public static string secretKey = "RDI2IHCXGQ3Y7CT1K2TIT2WM1L44INXX";
        public static string vnp_ApiUrl = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";

        public static string Md5(string message)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(message);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string Sha256(string message)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public static string HashAllFields(Dictionary<string, string> fields)
        {
            var sortedKeys = fields.Keys.OrderBy(k => k).ToList();
            var sb = new StringBuilder();
            for (int i = 0; i < sortedKeys.Count; i++)
            {
                string key = sortedKeys[i];
                string value = fields[key];
                if (!string.IsNullOrEmpty(value))
                {
                    sb.Append($"{key}={value}");
                    if (i < sortedKeys.Count - 1)
                        sb.Append("&");
                }
            }
            return HmacSHA512(secretKey, sb.ToString());
        }

        public static string HmacSHA512(string key, string data)
        {
            if (key == null || data == null)
            {
                throw new ArgumentNullException();
            }

            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] hash = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public static string GetIpAddress(HttpContext context)
        {
            string ipAddress = context.Request.Headers["X-FORWARDED-FOR"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = context.Connection.RemoteIpAddress?.ToString();
            }
            return ipAddress ?? "Unknown";
        }

        public static string GetRandomNumber(int len)
        {
            var rnd = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Range(0, len).Select(x => chars[rnd.Next(chars.Length)]).ToArray());
        }
    }

}
