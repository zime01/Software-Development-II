using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtp = new SmtpClient
            {
                Host = _configuration["Smtp:Host"],
                Port = int.Parse(_configuration["Smtp:Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"])
            };

            var message = new MailMessage(_configuration["Smtp:From"], to, subject, body)
            {
                IsBodyHtml = true
            };

            Console.WriteLine($"[Email service] sending email to {to}");

            await smtp.SendMailAsync(message);
        }
    }
}
