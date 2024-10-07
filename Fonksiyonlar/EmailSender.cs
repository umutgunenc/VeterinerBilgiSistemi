using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace VeterinerBilgiSistemi.Fonksiyonlar
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "umutdotnet@gmail.com";
        private readonly string _smtpPass = "rdbxuptguiuhjlto"; //"1989312caN.";

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_smtpUser);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;

            mailMessage.To.Add(email);

            var client = new SmtpClient(_smtpServer, _smtpPort);

            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Timeout = 10000; 
            await client.SendMailAsync(mailMessage);

        }
    }
}
