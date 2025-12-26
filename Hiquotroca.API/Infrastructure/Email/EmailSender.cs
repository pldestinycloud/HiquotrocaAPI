using Hiquotroca.API.Application.Interfaces;
using Resend;
using System.Net;
using System.Net.Mail;

namespace Hiquotroca.API.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IResend _resendClient;
        private readonly IConfiguration _config;

        public EmailSender(IResend resendClient, IConfiguration configuration)
        {
            _resendClient = resendClient;
            _config = configuration;
        }

        public async Task SendEmailAsync(string receipterAddress, string subject, string body)
        {
            var message = new EmailMessage();
            message.From = _config["Email:FromAddress"] ?? "";
            message.To.Add(receipterAddress);
            message.Subject = subject;
            message.HtmlBody = body;

            try
            {

                var result = await _resendClient.EmailSendAsync(message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
