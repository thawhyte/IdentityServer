using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheCoreBanking.Authenticate.Data.Model;
using TheCoreBanking.Authenticate.Models;

namespace TheCoreBanking.Authenticate.Controllers
{
    [Produces("application/json")]
    [Route("api/Subscription")]
    public class SubscriptionController : Controller
    {
        TheCoreBankingAuthenticateContext ValidateContext = new TheCoreBankingAuthenticateContext();
        TheCoreBankingAuthenticateContext db = new TheCoreBankingAuthenticateContext();

        // POST api/Subscription/AddSubscription

        [Route("~/api/Subscription/AddSubscription")]
        [HttpPost("AddSubscription")]
        public async Task<DemoResponse<string>> AddSubscription(TblSubscriptions subscriptions)
        {
            if (string.IsNullOrEmpty(subscriptions.TenantId) == true)
            {
                return DemoResponse<string>.GetResult("101", "Tenant Id is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.ApplicationId) == true)
            {
                return DemoResponse<string>.GetResult("101", "Application Id is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.ProductAlias) == true)
            {
                return DemoResponse<string>.GetResult("101", "Product alias is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.Amount) == true)
            {
                return DemoResponse<string>.GetResult("101", "Amount is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.CompanyId) == true)
            {
                return DemoResponse<string>.GetResult("101", "Company Id is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.CompanyAddress) == true)
            {
                return DemoResponse<string>.GetResult("101", "Company address is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.CompanyEmail) == true)
            {
                return DemoResponse<string>.GetResult("101", "Company email is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.CompanyName) == true)
            {
                return DemoResponse<string>.GetResult("101", "Company name is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.CompanyTelephone) == true)
            {
                return DemoResponse<string>.GetResult("101", "Company telephone is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.OrderDate) == true)
            {
                return DemoResponse<string>.GetResult("101", "Order date is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.OrderDetailDescription) == true)
            {
                return DemoResponse<string>.GetResult("101", "Order detail description is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.OrderId) == true)
            {
                return DemoResponse<string>.GetResult("101", "Order id is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.OrderDetailId) == true)
            {
                return DemoResponse<string>.GetResult("101", "Order detail id is empty");

            }
            if (string.IsNullOrEmpty(subscriptions.ValidityStartDate) == true)
            {
                return DemoResponse<string>.GetResult("101", "Validity start date is empty");
            }
            if (string.IsNullOrEmpty(subscriptions.ValidityEndDate) == true)
            {
                return DemoResponse<string>.GetResult("101", "Validity end date is empty");
            }
            var existTenant = db.TblSubscriptions.Where(o => o.TenantId == subscriptions.TenantId).Count();
            if (existTenant > 0)
            {
                return DemoResponse<string>.GetResult("111", "Existing");
            }
            await db.TblSubscriptions.AddAsync(subscriptions);
            if (db.SaveChanges() == 1)
            {
                if (subscriptions.CompanyEmail != null && subscriptions.TenantId != null)
                {
                    var token = subscriptions.TenantId;
                    var url = "http://fintrakbankingmmbs.azurewebsites.net/";
                    var username = subscriptions.CompanyName;
                    var password = "Password1";
                    // Email stuff
                    string body;
                    string subject = "FinTrak MMBS Portal";
                    using (var sr = new StreamReader(@"C:\Fintrak\SubscriptionTemplate.txt"))
                    {
                        body = sr.ReadToEnd();
                    }
                    string content = string.Format(body, subscriptions.ProductAlias, url, subscriptions.ApplicationId
                      );
                    string from = "fintrakcorrespondentbanking@gmail.com";
                    MailMessage message = new MailMessage(from, subscriptions.CompanyEmail);
                    message.Subject = subject;
                    message.Body = content;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = false;
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential("fintrakcorrespondentbanking@gmail.com", "admin@01");
                    //SmtpClient client = new SmtpClient
                    //{
                    //    Host = "smtp.gmail.com",
                    //    Port = 587,
                    //    EnableSsl = true,
                    //    UseDefaultCredentials = false,
                    //    Credentials = new System.Net.NetworkCredential("fintrakcorrespondentbanking@gmail.com", "admin@01"),
                    //    DeliveryMethod = SmtpDeliveryMethod.Network
                    //};

                    // Attempt to send the email
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Issue sending email: " + e.Message);
                    }
                }
                else // Email not found/
                {

                    ModelState.AddModelError("", "No user found by that email.");
                }
                return DemoResponse<string>.GetResult("000", "Successful");


            }
            else
            {
                return DemoResponse<string>.GetResult("110", "Other Error with Profiling Client Details");
            };
            //return DemoResponse<string>.GetResult(0, "OK");
        }
    }
}