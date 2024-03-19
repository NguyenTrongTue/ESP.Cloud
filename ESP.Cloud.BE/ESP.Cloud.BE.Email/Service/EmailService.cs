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

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"<html>
                                    <head>
                                        <style>
                                           .title{{
                                                color: red;
                                            }}
                                        </style>
                                    </head>
                                    <body>
                                        <h1 class='title'>Xin chào!</h1>
                                        <p>{emailDto.Body}</p>
                                    </body>
                                </html>";

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 465, true);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
