using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TheCoreBanking.Authenticate.Services
{
    public class EmailService: IEmailService
    {
        //private readonly IConfiguration _configuration;
        //public EmailService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public async Task SendEmail(string email,string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "erp-notification@nibss-plc.com.ng",
                    Password = "nERP@88Ha"
                };
                client.Credentials = credential;
                client.Host = "192.168.202.223";
                client.Port = int.Parse("25");
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    
                    string body = "<table width='100%' border='0' cellspacing='0' cellpadding='20' background='/App_Data/fintrakimage.jpg'> " +
                        "<tr>" +
                        "<td>" +
                        "<p> " +
                        "Hi,<p>We received a request to reset your password through your email address.You can change your password using the following link:</p>" + message + "<p>Kindly click the link<p/><p> If you did not initiate this request, please ignore this email. Your password will not change until you access the link above and create a new one.<b>Do not reset</b>" + "<p>Sincerely yours,</p>" + "<p>The Fintrak Banking Team</p>" +
                        "</p>" +
                        "<p>" +
                        "FINTRAK Core Banking email Disclaimer and confidentiality note" +
                        "</p>" +
                         "<p>" +
                        "This e-mail, its attachments and any rights attaching hereto are, unless the content clearly indicates<p>otherwise, the property of FINTRAK Core Banking and its subsidiaries </p> <p>It is confidential, private and intended for only the addressee </p>" +
                        "</p>" +
                        "</td>" +
                        "</tr> " +
                        "</table> ";

                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress("erp-notification@nibss-plc.com.ng");
                    emailMessage.Subject = "FINTRAK CORE BANKING: Forgot Password";
                    emailMessage.Body = body;
                    emailMessage.BodyEncoding = Encoding.UTF8;
                    emailMessage.IsBodyHtml = true;
                    await client.SendMailAsync(emailMessage);
                }
            }
            await Task.CompletedTask;
        }

        
    }
}
