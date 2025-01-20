using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "ronaldgallardoarce78@gmail.com";
        private readonly string _smtpPassword = "oxwa ydio rlfi wnkh";
        public async Task SendEmailAsync(EmailToSend emailToSend)
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
                    client.EnableSsl = true;
                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(_smtpUser),
                        Subject = emailToSend.Subject,
                        Body = emailToSend.Body,
                        IsBodyHtml = true
                    };
                    mail.To.Add(emailToSend.To);
                    await client.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                throw;
            }
        }
    }
}
