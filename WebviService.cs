using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Net.Mail;
using WebviAPI.IServices;
using WebviAPI.Model;

namespace WebviAPI.Services
{
    public class WebviService : IWebviService
    {
        private readonly IConfiguration _config;

        public WebviService(IConfiguration config)
        {
            _config = config;
        }

        public void SandEmail(ContantFrom form)
        {
            var fromEmail = _config["EmailSettings:From"];
            var fromPassword = _config["EmailSettings:Password"];
            var toEmail = _config["EmailSettings:To"];
            var host = _config["EmailSettings:Host"];
            var port = int.Parse(_config["EmailSettings:Port"] ?? "587");

            if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(fromPassword) || string.IsNullOrEmpty(toEmail))
                throw new Exception("Email configuration is missing in appsettings.json");

            using (var smtp = new SmtpClient(host, port))
            {
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);

                var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = $"New message from {form.Name}",
                    Body = $"Name: {form.Name}\nEmail: {form.Email}\nMessage: {form.Message}"
                };

                smtp.Send(message);
            }
        }
    }
}

