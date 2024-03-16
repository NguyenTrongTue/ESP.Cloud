using ESP.Cloud.BE.Email.Interface;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ESP.Cloud.BE.Email
{
    public class EmailService : IEmail
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendMail(EmailDto emailDto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(emailDto.To));
            email.Subject = emailDto.Subject;
            var buttonUrl = "https://www.facebook.com/";
            var buttonHtml = $@"<button type='button'><a href='{buttonUrl}'>Click here</a></button>";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"<html>
                                    <head>
                                        <style>
                                           
                                        </style>
                                    </head>
                                    <body>
                                        <h1>Xin chào!</h1>
                                        <p>{emailDto.Body}</p>
                                        <p>{buttonHtml}</p>
                                    </body>
                                </html>";

            // Thêm phần body vào email
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();


            smtp.Connect(_config.GetSection("EmailHost").Value, 465, true);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
