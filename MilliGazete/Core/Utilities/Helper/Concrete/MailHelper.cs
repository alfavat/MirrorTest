using System;
using System.Net;
using System.Net.Mail;
using Core.Utilities.Helper.Abstract;
using Microsoft.Extensions.Configuration;
namespace Core.Utilities.Helper
{
    public class MailHelper : IMailHelper
    {
        public IConfiguration Configuration { get; }
        public MailHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public bool SendEmail(string subject,string body, string[] to)
        {
            try
            {
                var mailSettings = Configuration.GetSection("EmailSettings").Get<EmailSettings>();

                var message = new MailMessage();
                var smtp = new SmtpClient();
                message.From = new MailAddress(mailSettings.From);
                foreach (var item in to)
                {
                    message.To.Add(new MailAddress(item));
                }
                message.Subject = subject;
                message.IsBodyHtml = mailSettings.IsBodyHtml;
                message.Body = body;
                smtp.Port = mailSettings.Port;
                smtp.Host = mailSettings.Host;
                smtp.EnableSsl = mailSettings.EnableSsl;
                smtp.UseDefaultCredentials = mailSettings.UseDefaultCredentials;
                smtp.Credentials = new NetworkCredential(mailSettings.UserName, mailSettings.Password);
                smtp.DeliveryMethod = (SmtpDeliveryMethod)mailSettings.SmtpDeliveryMethod;
                smtp.Send(message);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }


}
