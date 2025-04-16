using AuthExcelService.Repositoies.Contracts.IEmaiService;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.RepositoryService.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var client = new SmtpClient
            {
                Host = _smtpSettings.Host,
                Port = _smtpSettings.Port,
                EnableSsl = _smtpSettings.EnableSsl,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.FromEmail, _smtpSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
        }
    }


    public class SmtpSettings
    {
        public string Host { get; set; } // SMTP server host (e.g., smtp.gmail.com)
        public int Port { get; set; } // SMTP server port (e.g., 587 for TLS, 465 for SSL)
        public bool EnableSsl { get; set; } // Whether to enable SSL/TLS
        public string Username { get; set; } // SMTP username (e.g., your email address)
        public string Password { get; set; } // SMTP password or app password
        public string FromEmail { get; set; } // Sender's email address
        public string FromName { get; set; } // Sender's display name
    }

}
