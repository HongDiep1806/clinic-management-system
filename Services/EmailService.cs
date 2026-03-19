using System.Net.Mail;
using System.Net;

namespace ClinicManagementSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var email = _config["EmailSettings:Email"];
                var password = _config["EmailSettings:Password"];

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                var mail = new MailMessage(email, to, subject, body);
                mail.IsBodyHtml = true;

                await smtp.SendMailAsync(mail);

                Console.WriteLine("EMAIL SENT SUCCESSFULLY");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EMAIL ERROR: " + ex.Message);
                throw;
            }
        }
    }
}
