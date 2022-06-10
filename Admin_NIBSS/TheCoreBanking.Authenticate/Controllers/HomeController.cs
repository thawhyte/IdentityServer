// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheCoreBanking.Authenticate.Services;
using TheCoreBanking.Authenticate.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.SignalR;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Model;
using System.Linq;

namespace TheCoreBanking.Authenticate.Controllers
{
   // [SecurityHeaders]
    public class HomeController : Controller
    {        
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IStringLocalizer<HomeController> _localizer;
        private IAuthenticateUnitOfWork AuthenticateUnitOfWork { get; }

        private readonly TheCoreBankingAuthenticateContext _context = new TheCoreBankingAuthenticateContext();
        public HomeController(IIdentityServerInteractionService interaction, IStringLocalizer<HomeController> localizer, IAuthenticateUnitOfWork uow)
        {
            _localizer = localizer;
            _interaction = interaction;
            AuthenticateUnitOfWork = uow;


        }
        [Authorize]
        //[AllowAnonymous]
        public IActionResult Index()
        {
            var CurrentDates = AuthenticateUnitOfWork.CurrentDate.GetAll();
            var CurrentDate = CurrentDates.FirstOrDefault().CurrentDate;
            ViewBag.CurrentDate = string.Format("{0:dddd MMM dd,yyyy}", CurrentDate);
            return View();

            //ViewBag.CurrentDate = AuthenticateUnitOfWork.CurrentDate.GetAll().Select(o => o.CurrentDate);            
            //return View();
        }



        public IActionResult SetCulture(string id = "en")
        {
            string culture = id;
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
           );

            ViewData["Message"] = "Culture set to " + culture;

            return View("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        public class Chat : Hub
        {
            public async Task SendMessage(string user, string message)
            {
                var sender = Context.User?.Identity?.Name ?? "anonymous";
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }

        }
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Main.Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }


        
    }
}