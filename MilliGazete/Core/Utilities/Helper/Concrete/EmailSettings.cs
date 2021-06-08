using System.Net.Mail;

namespace Core.Utilities.Helper
{
    public class EmailSettings
    {
        public string From { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool EnableSsl { get; set; } = true;
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBodyHtml { get; set; } = true;
        public bool UseDefaultCredentials { get; set; }
        public int SmtpDeliveryMethod { get; set; } = 0;
        public string Url { get; set; }
    }
}
