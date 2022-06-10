using IdentityModel;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using TheCoreBanking.Authenticate.Services;
using TheCoreBanking.Authenticate.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using IdentityServer4;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using TheCoreBanking.Authenticate.Data.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.Extensions.Configuration;
using RestSharp.Authenticators;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheCoreBanking.Authenticate.Controllers
{
    [Route("api/login")]
    [ApiController]
    
    public class RemoteAPIController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly AccountService _account;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly string apiBaseUrl;
        private readonly IConfiguration _Configure;
        // GET: api/<RemoteAPIController>
        public RemoteAPIController
            (
             IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationSchemeProvider schemeProvider, IConfiguration Configuration,
            IEventService events,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IEmailService emailService
            )
        {
             _userManager = userManager;
            _interaction = interaction;
            _Configure = Configuration;
            _events = events;
            _account = new AccountService(interaction, httpContextAccessor, schemeProvider, clientStore);
            _signInManager = signInManager;
            _emailService = emailService;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            apiBaseUrl = _Configure.GetValue<string>("NibbsBaseUrl");

        }

       

        public List<string> GetLoginDetails(LoginInputModel model)
        {
            List<string> listArray = new List<string>();
            try
            {

                var client = new RestClient("http://10.91.91.136/singleauth/login/auth-only");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                client.Authenticator = new HttpBasicAuthenticator(model.Username, model.Password);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    string results = @response.Content.ToString();
                    AdUserModel adUser = JsonConvert.DeserializeObject<AdUserModel>(results);
                    var msg = adUser.meta.message;
                    return listArray = new List<string>() { adUser.data.givenName, adUser.data.sAMAccountName, adUser.data.name, adUser.data.userPrincipalName };
                }
                // }
            }
            catch (Exception)
            {

                throw;
            }
            listArray = new List<string>() { };
            return listArray;
        }
        public int DeleteAspNetUser(string UserName)
        {

            using (var context = new TheCoreBankingAuthenticateContext())
            {
                SqlParameter _userName = new SqlParameter("@Username", UserName);

                return context.Database.ExecuteSqlRaw("dbo.DeleteAspNetUser @Username", _userName);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            TheCoreBankingAuthenticateContext db = new TheCoreBankingAuthenticateContext();

            if (button != "login")
            {
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);
                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null

                    return LocalRedirect(model.ReturnUrl);
                }
                else
                {
                    return Redirect("~/");
                }
            }
            if (ModelState.IsValid)
            {

                var user = GetLoginDetails(model).ToList();
                var userDetails = await _userManager.FindByNameAsync(model.Username);
                if (user.Count >= 0)
                {
                    var existUser = db.TblStaffInformation.Where(o => o.StaffName == model.Username).Count();
                    if (existUser == 0)
                    {
                        Models.TblStaffInformation tblStaff = new Models.TblStaffInformation
                        {
                            StaffName = user[1],
                            Address = "Nibss",
                            Age = DateTime.Now,
                            BranchId = "101",

                            CompanyId = "1",

                            Email = user[3],




                            Locked = "false",
                            Status = "Active"

                        };

                        var newUser = new ApplicationUser
                        {
                            UserName = user[1],
                            tblStaffInformation = tblStaff,
                            Email = user[3],
                            PhoneNumber = "",


                        };
                        var getAspNetUser = await _userManager.FindByNameAsync(model.Username);
                        if (getAspNetUser != null)
                        {
                            int countdel = DeleteAspNetUser(model.Username);
                        }
                        model.Password = model.Password + "@";
                        var result_set = _userManager.CreateAsync(newUser, model.Password).Result;
                        if (result_set.Succeeded)
                        {

                            if (user != null && await _userManager.CheckPasswordAsync(newUser, model.Password))
                            {
                                await _events.RaiseAsync(
                                    new UserLoginSuccessEvent(newUser.UserName, newUser.Id, newUser.UserName));

                                AuthenticationProperties props = null;

                                props = new AuthenticationProperties
                                {
                                    IsPersistent = false,
                                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                                };


                                await HttpContext.SignInAsync(newUser.Id, newUser.UserName, props);

                                //if (_interaction.IsValidReturnUrl(model.ReturnUrl)
                                //        || Url.IsLocalUrl(model.ReturnUrl))
                                //{
                                //    return Redirect(model.ReturnUrl);

                                //}
                                Data.Model.TblBankingAuditTrail tblBankingAudits = new Data.Model.TblBankingAuditTrail();

                                tblBankingAudits.TransType = "Login to ERP";
                                tblBankingAudits.ErrorCode = "Successful";
                                tblBankingAudits.UserName = string.IsNullOrEmpty(user[1]) ? model.Username : user[1];
                                AuditLog(tblBankingAudits);

                                return Redirect("~/Home/Index");

                            }

                            else
                            {

                                Data.Model.TblBankingAuditTrail tblBankingAudits = new Data.Model.TblBankingAuditTrail();
                                tblBankingAudits.TransType = $"{newUser.UserName} TRIED TO LOGIN TO ERP MODULE";

                                tblBankingAudits.UserName = newUser.UserName;
                                AuditLog(tblBankingAudits);

                                throw new Exception(result_set.Errors.First().Description);

                            }
                        }
                    }
                    // var result_set =  _userManager.CheckPasswordAsync(userDetails, model.Password).Result;
                    if (existUser > 0)
                    {
                        var existUse = db.TblStaffInformation.Where(o => o.StaffName == model.Username).FirstOrDefault();
                        if (existUse.FullName == null || existUse.FullName == "")
                        {
                            existUse.FullName = user[2];
                            db.Update(existUse);
                            db.SaveChanges();
                        }

                        await _events.RaiseAsync(
                            new UserLoginSuccessEvent(userDetails.UserName, userDetails.Id, userDetails.UserName));

                        AuthenticationProperties props = null;

                        props = new AuthenticationProperties
                        {
                            IsPersistent = false,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };


                        await HttpContext.SignInAsync(userDetails.Id, userDetails.UserName, props);

                        if (_interaction.IsValidReturnUrl(model.ReturnUrl)
                                || Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);

                        }

                        Data.Model.TblBankingAuditTrail tblBankingAudits = new Data.Model.TblBankingAuditTrail();
                        tblBankingAudits.TransType = "Login to ERP";
                        tblBankingAudits.ErrorCode = "Successful";
                        tblBankingAudits.UserName = string.IsNullOrEmpty(user[1]) ? model.Username : user[1];
                        AuditLog(tblBankingAudits);
                        return Redirect("~/Home/Index");

                    }



                }
                Data.Model.TblBankingAuditTrail tblBankingAudit = new Data.Model.TblBankingAuditTrail();

                tblBankingAudit.TransType = $"{model.Username} just entered invalid credentials";

                tblBankingAudit.UserName = model.Username;
                AuditLog(tblBankingAudit);

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }

             return (IActionResult)await _account.BuildLoginViewModelAsync(model);
            //return View(vm);
        }
        public string AuditLog(Data.Model.TblBankingAuditTrail tblBankingAudit)
        {
            string logUser;
            logUser = tblBankingAudit.UserName;
            TheCoreBankingAuthenticateContext db = new TheCoreBankingAuthenticateContext();
            var customer = db.TblStaffInformation.Where(o => o.StaffName == logUser).FirstOrDefault();
            var request = HttpContext.Request;
            var IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
            // The URL that was accessed
            var AreaAccessed = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
            // Creates Timestamp
            var TimeStamp = DateTime.Now;
            tblBankingAudit.Ipaddress = IPAddress;
            tblBankingAudit.TransDate = TimeStamp;
            tblBankingAudit.UserName = logUser;
            tblBankingAudit.BrCode = customer.BranchId;
            tblBankingAudit.CoyCode = customer.CompanyId;

            tblBankingAudit.TransTime = TimeStamp.ToShortTimeString();
            tblBankingAudit.Status = "true";

            db.TblBankingAuditTrail.Add(tblBankingAudit);
            db.SaveChanges();

            return "Successful";

        }

     
        


    }
}
