using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RestaurantApi.Core.Application.DTOs.Email;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Domain.Settings;

namespace RestaurantApi.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings _mailSettings { get; }
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendAsync(EmailRequest emailRequest)
        {
            try
            {
                MimeMessage email = new();
                email.From.Add(new MailboxAddress(_mailSettings.FromName, _mailSettings.From));
                email.To.Add(MailboxAddress.Parse(emailRequest.To));
                email.Subject = emailRequest.Subject;
                BodyBuilder builder = new() { HtmlBody = emailRequest.Body };
                email.Body = builder.ToMessageBody();

                using SmtpClient smtp = new();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.User, _mailSettings.Pass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex) { }
        }
    }
}
