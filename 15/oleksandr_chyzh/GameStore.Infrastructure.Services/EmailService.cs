using System.Net;
using System.Net.Mail;
using GameStore.Services.Interfaces;

namespace GameStore.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string emailTo, string subject, string body)
        {
            var from = new MailAddress("coolgamestore@gmail.com", "GameStore");
            var to = new MailAddress(emailTo);
            var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("coolgamestore@gmail.com", "zxcvbnm,./"),
                EnableSsl = true
            };

            smtp.Send(message);
        }
    }
}
