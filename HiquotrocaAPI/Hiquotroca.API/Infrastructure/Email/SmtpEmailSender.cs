using Hiquotroca.API.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Hiquotroca.API.Infrastructure.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var from = _config["Email:From"];
            var host = _config["Email:SmtpHost"];
            var port = int.Parse(_config["Email:SmtpPort"] ?? "587");
            var user = _config["Email:User"];
            var pass = _config["Email:Password"];

            using var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(user, pass)
            };

            using var message = new MailMessage(from!, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(message);
        }
    }
}
