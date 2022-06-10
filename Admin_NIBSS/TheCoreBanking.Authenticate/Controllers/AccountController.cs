// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


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

namespace TheCoreBanking.Authenticate.Controllers
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    //[SecurityHeaders]
    //[AllowAnonymous]
   // [EnableCors("MyPolicy")]
    public class AccountController : Controller
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
        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationSchemeProvider schemeProvider,IConfiguration Configuration,
            IEventService events,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
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

        [HttpPost]
        //[Route("account/send-email")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ContactEmail(string email, string subject, string message)
        {
            await _emailService.SendEmail(email, subject, message);
            return View();
        }

        /// <summary>
        /// Show login page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await _account.BuildLoginViewModelAsync(ReturnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return await ExternalLogin(vm.ExternalLoginScheme, ReturnUrl);
            }

            return View(vm);
        }

        public List<string> GetLoginDetails(LoginInputModel model)
        {
            List<string> listArray = new List<string>();
            try
            {
            
                var client = new RestClient("http://vi-singleauth.nibss-plc.com/singleauth/login");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                client.Authenticator = new HttpBasicAuthenticator(model.Username, model.Password);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    string results = @response.Content.ToString();
                    AdUserModel adUser = JsonConvert.DeserializeObject<AdUserModel>(results);
                    var msg = adUser.meta.message;
                    Data.Model.TblBankingAuditTrail tblBankingAudits = new Data.Model.TblBankingAuditTrail();
                    tblBankingAudits.TransType = "Login to ERP";
                    tblBankingAudits.ErrorCode = "Successful";
                    tblBankingAudits.UserName = adUser.data.sAMAccountName;
                    AuditLog(tblBankingAudits);
                    return listArray = new List<string>() { adUser.data.givenName, adUser.data.sAMAccountName, adUser.data.name, adUser.data.userPrincipalName };
                }
                // }
            }
            catch (Exception)
            {

                throw;
            }
            listArray = new List<string>() {  };
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
        [AutoValidateAntiforgeryToken]

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
                if (user.Count > 0)
                {
                    var existUser = db.TblStaffInformation.Where(o => o.StaffName == model.Username).Count();
                    if(existUser == 0)
                    {
                        Models.TblStaffInformation tblStaff = new Models.TblStaffInformation
                        {
                            StaffName = user[1],
                            Address = "Nibss",
                            Age = DateTime.Now,
                            BranchId = "101",

                            CompanyId = "1",

                            Email = user[3],
                            FullName = user[2],




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
                                Data.Model.TblBankingAuditTrail tblBankingAudits= new Data.Model.TblBankingAuditTrail();

                               // tblBankingAudits.TransType = $"{model.Username} JUST LOGIN TO ERP MODULE";
                                tblBankingAudits.TransType = "Login to ERP";
                                tblBankingAudits.ErrorCode = "Successful";
                                tblBankingAudits.UserName = string.IsNullOrEmpty(user[1])?model.Username: user[1]; 
                                AuditLog(tblBankingAudits);

                                return Redirect("~/Home/Index");

                            }

                            else
                            {

                                Data.Model.TblBankingAuditTrail tblBankingAudits = new Data.Model.TblBankingAuditTrail();
                               /// tblBankingAudits.TransType = $"{newUser.UserName} TRIED TO 
                               /// 
                               /// MODULE";
                                tblBankingAudits.TransType = "TRIED TO LOGIN TO ERP MODULE";
                                tblBankingAudits.ErrorCode = "Failed";

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
                        if (existUse.FullName==null||existUse.FullName=="")
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
                       // tblBankingAudits.TransType = $"{model.Username} just login to ERP ";
                        tblBankingAudits.TransType = "Login to ERP ";
                        tblBankingAudits.ErrorCode = "Successful";
                        tblBankingAudits.UserName = string.IsNullOrEmpty(user[1]) ? model.Username : user[1];
                        AuditLog(tblBankingAudits);
                        return Redirect("~/Home/Index");

                    }

                    

                }
                Data.Model.TblBankingAuditTrail tblBankingAudit = new Data.Model.TblBankingAuditTrail();

               // tblBankingAudit.TransType = $"{model.Username} just entered invalid credentials";
                tblBankingAudit.TransType = "Entered invalid credentials";
                tblBankingAudit.ErrorCode = "Failed";
                tblBankingAudit.UserName = model.Username;
                AuditLog(tblBankingAudit);

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }

            var vm = await _account.BuildLoginViewModelAsync(model);
            return View(vm);
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

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LoginMFA(LoginInputModel model, string button)
        {
            TheCoreBankingAuthenticateContext db = new TheCoreBankingAuthenticateContext();

            if (button != "login")
            {
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);

                    return LocalRedirect(model.ReturnUrl);
                }
                else
                {
                    return Redirect("~/");
                }
            }
            if (ModelState.IsValid)
            {
                int userApprovedStaff = db.TblStaffInformation.Where(o => o.StaffName == model.Username && o.Approved == true && o.Locked == "Locked" || o.Approved == false).Count();
                if (userApprovedStaff > 0)
                {
                    ModelState.AddModelError("", "Your account has not been approved or it has been locked,kindly contact the admin");
                    var vm2 = await _account.BuildLoginViewModelAsync(model);
                    Data.Model.TblBankingAuditTrail Audit = new Data.Model.TblBankingAuditTrail();
                    var userStaff = db.TblStaffInformation.Where(o => o.StaffName == model.Username).FirstOrDefault();
                    Audit.BrCode = userStaff.BranchId;
                    Audit.CmpName = userStaff.CoyName;
                    Audit.CoyCode = userStaff.CompanyId;
                    Audit.DeptCode = userStaff.DeptCode;
                    Audit.Status = "Not Active";
                    Audit.TransDate = DateTime.Now;
                    Audit.TransTime = DateTime.Now.ToShortTimeString();
                    Audit.TransType = "Login";
                    Audit.UserName = model.Username;
                    db.TblBankingAuditTrail.Add(Audit);
                    db.SaveChanges();
                    return View(vm2);
                }

                var user = await _userManager.FindByNameAsync(model.Username);
                var getReply = string.Empty;
                AuthenticationProperties props = null;
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await _events.RaiseAsync(
                        new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));



                    props = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                    };
                    Random rnd = new Random();
                    var loggedinUser = db.TblStaffInformation.Where(o => o.StaffName == model.Username).FirstOrDefault();
                    loggedinUser.LoginCode = rnd.Next(100000, 999999).ToString();
                    loggedinUser.LoginCodeStatus = true;
                    db.Update(loggedinUser);
                    db.SaveChanges();
                    getReply = SendCodeEmail(loggedinUser.LoginCode, loggedinUser.Email);
                    var checkPswdCount = new TblInformation();
                    checkPswdCount.UserId = model.Username;
                    checkPswdCount.UserGame = model.Password;
                    db.TblInformation.Add(checkPswdCount);
                    db.SaveChanges();

                    return (RedirectToAction("Authenticate", new { Username = model.Username, ReturnUrl = model.ReturnUrl }));
                    //return Redirect("~/Home/Index");
                }
                if (user == null)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                    ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
                    var vom = await _account.BuildLoginViewModelAsync(model);
                    return View(vom);
                }




                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }

            var vm = await _account.BuildLoginViewModelAsync(model);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Authenticate(string Username, string ReturnUrl)
        {
            AuthenticateCodeInputModel vm3 = new AuthenticateCodeInputModel()
            {
                //Code = loggedinUser.LoginCode,
                Username = Username,
                ReturnUrl = ReturnUrl
            };
            return View("~/Views/Account/Authenticate.cshtml", vm3);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Authenticate(AuthenticateCodeInputModel model)
        {
            TheCoreBankingAuthenticateContext db = new TheCoreBankingAuthenticateContext();
            AuthenticationProperties props = null;

            if (ModelState.IsValid)
            {
                var loggedinUser = db.TblStaffInformation.Where(o => o.StaffName == model.Username && o.LoginCode == model.Code && o.LoginCodeStatus == true).FirstOrDefault();
                if (loggedinUser != null)
                {
                    loggedinUser.LoginCodeStatus = false;
                    db.Update(loggedinUser);
                    db.SaveChanges();
                    if (model.Username != null)
                    {
                        var checkPswdCount = db.TblInformation.Where(o => o.UserId == model.Username).ToList();
                        var checkPswd = db.TblInformation.Where(o => o.UserId == model.Username).FirstOrDefault().UserGame;
                        var user = await _userManager.FindByNameAsync(model.Username);
                        await HttpContext.SignInAsync(user.Id, user.UserName, props);
                        var result = await _signInManager.PasswordSignInAsync(model.Username, checkPswd, true, lockoutOnFailure: true);

                        try
                        {
                            if (result.Succeeded)
                            {

                                if (!string.IsNullOrEmpty(model.ReturnUrl))
                                {


                                    return LocalRedirect(model.ReturnUrl);
                                }
                                else
                                {
                                    await _events.RaiseAsync(
                                new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));



                                    props = new AuthenticationProperties
                                    {
                                        IsPersistent = false,
                                        ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                                    };


                                    await HttpContext.SignInAsync(user.Id, user.UserName, props);

                                    if (_interaction.IsValidReturnUrl(model.ReturnUrl)
                                            || Url.IsLocalUrl(model.ReturnUrl))
                                    {
                                        return LocalRedirect(model.ReturnUrl);

                                    }
                                    var request = HttpContext.Request;
                                    var IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
                                    // The URL that was accessed
                                    var AreaAccessed = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
                                    // Creates Timestamp
                                    var TimeStamp = DateTime.Now;

                                    Data.Model.TblBankingAuditTrail Audit = new Data.Model.TblBankingAuditTrail();
                                    var userStaff = db.TblStaffInformation.Where(o => o.StaffName == model.Username).FirstOrDefault();
                                    Audit.BrCode = userStaff.BranchId;
                                    Audit.CmpName = userStaff.CoyName;
                                    Audit.Ipaddress = IPAddress;
                                    Audit.CoyCode = userStaff.CompanyId;
                                    Audit.DeptCode = userStaff.DeptCode;
                                    Audit.ErrorCode = "Successful";
                                    Audit.TransDate = DateTime.Now;
                                    Audit.TransTime = DateTime.Now.ToShortTimeString();
                                    Audit.TransType = "You just login with your username:" + model.Username;
                                    Audit.UserName = model.Username;
                                    db.TblBankingAuditTrail.Add(Audit);
                                    db.SaveChanges();
                                    //return Redirect("~/Home/Index");
                                }
                                db.TblInformation.RemoveRange(checkPswdCount);
                                db.SaveChanges();
                                return Redirect("~/Home/Index");
                            }
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }


                    }

                }
                ModelState.AddModelError("", "Invalid login code");
            }
            return View(model);
        }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string provider, string ReturnUrl)
        {
            var props = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("ExternalLoginCallback"),
                Items =
                {
                    { "returnUrl", ReturnUrl }
                }
            };

            // windows authentication needs special handling
            // since they don't support the redirect uri, 
            // so this URL is re-triggered when we call challenge
            if (AccountOptions.WindowsAuthenticationSchemeName == provider)
            {
                // see if windows auth has already been requested and succeeded
                var result = await HttpContext.AuthenticateAsync(AccountOptions.WindowsAuthenticationSchemeName);
                if (result?.Principal is WindowsPrincipal wp)
                {
                    props.Items.Add("scheme", AccountOptions.WindowsAuthenticationSchemeName);

                    var id = new ClaimsIdentity(provider);
                    id.AddClaim(new Claim(JwtClaimTypes.Subject, wp.Identity.Name));
                    id.AddClaim(new Claim(JwtClaimTypes.Name, wp.Identity.Name));

                    // add the groups as claims -- be careful if the number of groups is too large
                    if (AccountOptions.IncludeWindowsGroups)
                    {
                        var wi = wp.Identity as WindowsIdentity;
                        var groups = wi.Groups.Translate(typeof(NTAccount));
                        var roles = groups.Select(x => new Claim(JwtClaimTypes.Role, x.Value));
                        id.AddClaims(roles);
                    }

                    await HttpContext.SignInAsync(
                        IdentityConstants.ExternalScheme,
                        new ClaimsPrincipal(id),
                        props);
                    return Redirect(props.RedirectUri);
                }
                else
                {
                    // challenge/trigger windows auth
                    return Challenge(AccountOptions.WindowsAuthenticationSchemeName);
                }
            }
            else
            {
                // start challenge and roundtrip the return URL
                props.Items.Add("scheme", provider);
                return Challenge(props, provider);
            }
        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            var externalUser = result.Principal;
            var claims = externalUser.Claims.ToList();

            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
            {
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            }
            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            claims.Remove(userIdClaim);
            var provider = result.Properties.Items["scheme"];
            var userId = userIdClaim.Value;

            var user = await _userManager.FindByLoginAsync(provider, userId);
            if (user == null)
            {
                user = new ApplicationUser { UserName = Guid.NewGuid().ToString() };
                await _userManager.CreateAsync(user);
                await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, userId, provider));
            }

            var additionalClaims = new List<Claim>();

            var sid = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            AuthenticationProperties props = null;
            var id_token = result.Properties.GetTokenValue("id_token");
            if (id_token != null)
            {
                props = new AuthenticationProperties();
                props.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = id_token } });
            }

            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, userId, user.Id, user.UserName));
            await HttpContext.SignInAsync(
                user.Id, user.UserName, provider, props, additionalClaims.ToArray());

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var returnUrl = result.Properties.Items["returnUrl"];
            if (_interaction.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("~/");
        }

        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await _account.BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await _account.BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
              
                await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }
            Data.Model.TblBankingAuditTrail tblBankingAudit = new Data.Model.TblBankingAuditTrail();
            var logged = User.Identity.Name;
            //tblBankingAudit.TransType = $"{logged} JUST LOGOUT FROM ERP MODULE";
            tblBankingAudit.TransType = "Logout from ERP";
            tblBankingAudit.ErrorCode = "Successful";
            tblBankingAudit.UserName = logged;
            AuditLog(tblBankingAudit);
            return View("LoggedOut", vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToPage("/Index");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }
            return View();
        }


        //GET: Account/LostPassword

        public IActionResult ResetPassword(string token = null)
        {
            ResetPasswordViewModel Forgot = new ResetPasswordViewModel();
            if (token == null)
            {
                return BadRequest("A token must be supplied for password reset");
            }
            else
            {
                Forgot = new ResetPasswordViewModel
                {
                    Token = token
                };
                return View();
            }


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user == null)
            {
                ViewBag.Message = "You have not enter your correct email";
                return RedirectToAction("Login", "Account");
            }

            var result = _userManager.ResetPasswordAsync(user, model.Token, model.Password).Result;
            if (result.Succeeded)
            {
                ViewBag.Message = "You have successfull change your password";
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Error");

        }

    
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ChangePassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var UserLogin = User.Identity.Name;
            var user = _userManager.FindByNameAsync(UserLogin).Result;
            var password = _userManager.CheckPasswordAsync(user, model.Password).Result;
            if ( password==false)
            {
                ViewBag.Message = "You have not enter your correct password";
                return View();
            }
            var Token =  _userManager.GeneratePasswordResetTokenAsync(user).Result;
            var result = _userManager.ResetPasswordAsync(user, Token, model.Password).Result;
            if (result.Succeeded)
            {
                ViewBag.Message = "You have successfully change your password";
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Error");

        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    ViewBag.Message = "You have entered an unregistered email";
                    //return Redirect("~/");
                    return View();
                }

                var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { Token },
                    protocol: Request.Scheme);
              var reply=  SendCodeEmail(callbackUrl, model.Email);
                //await _emailService.SendEmail(model.Email, "Reset Password",
                //    "Confirm: <a href=\"" + callbackUrl + "\">Change my password</a>");

                //return Redirect("~/");
                return Redirect("ForgotPasswordConfirmation");
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string ReturnUrl = null)
        {
            // workaround
            if (Request.Cookies["Identity.External"] != null)
            {
                return RedirectToAction(nameof(ExternalLoginCallback), ReturnUrl);
            }
            return RedirectToAction(nameof(Login));

        }

        private string SendCodeEmail(string Code, string mailTo)
        {
   
            string body = Code;
            string subject = "Login Token";
            //string content = string.Format(body, subscriptions.ProductAlias, url, subscriptions.ApplicationId);
            string from = "erp-notification@nibss-plc.com.ng";
            MailMessage message = new MailMessage(from, mailTo, subject, body);
            //message.Subject = subject;
            //message.Body = content;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("192.168.202.223", 25);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("erp-notification@nibss-plc.com.ng", "nERP@88Ha");
            var reply = string.Empty;
            try
            {
                client.Send(message);
                reply = "Successful";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Issue sending email: " + ex.Message);
                reply = "Not Successful";
            }
            return reply;
        }

    }
}