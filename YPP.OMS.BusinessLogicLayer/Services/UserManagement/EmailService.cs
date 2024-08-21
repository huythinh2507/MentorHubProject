using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace YPP.MH.BusinessLogicLayer.Services.UserManagement
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendConfirmationEmailAsync(string email, string code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["Email:SenderName"], _configuration["Email:Sender"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = _configuration["Email:Subject"];

            message.Body = new TextPart("plain")
            {
                Text = _configuration["Email:BodyTemplate"].Replace("{code}", code)
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_configuration["Email:SmtpServer"], 587, false);

                await client.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);

                await client.SendAsync(message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["Email:SenderName"], _configuration["Email:Sender"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Password Reset Request";
            message.Body = new TextPart("plain")
            {
                Text = $"To reset your password, please click on this link: {resetLink}"
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_configuration["Email:SmtpServer"], 587, false);
                await client.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
                await client.SendAsync(message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
