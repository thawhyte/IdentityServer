// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TheCoreBanking.Authenticate.Data;
using TheCoreBanking.Authenticate.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using TheCoreBanking.Authenticate.Models;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Microsoft.Extensions.Logging;
using TheCoreBanking.Data.Contracts;
using TheCoreBanking.Data.Repository;
using TheCoreBanking.Data;
using Microsoft.AspNetCore.Http;
using IdentityServer4.Services;
using IdentityServer4.EntityFramework.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using TheCoreBanking;
using TheCoreBanking.Authenticate.Data.Contracts;
using TheCoreBanking.Authenticate.Data.Helpers;
using IdentityServer4.Configuration;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Security.Cryptography;
using Microsoft.Azure.Services.AppAuthentication;
using System.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using AntiXssMiddleware.Middleware;
using TheCoreBanking.Authenticate.ConfigurationOptions;
using TheCoreBanking.Authenticate.Extensions;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TheCoreBanking.Authenticate
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        private AppSettings AppSettings { get; set; }

        string Origin = "https://nibsserpworkflow.nibss-plc.com.ng,https://nibsserpexpense.nibss-plc.com.ng,https://erp.nibss-plc.com.ng/finances,https://nibsserpvendor.nibss-plc.com.ng,https://nibsserpfixedasset.nibss-plc.com.ng,https://nibsserpdoc.nibss-plc.com.ng,https://nibsserpinventory.nibss-plc.com.ng,https://budget.nibss-plc.com.ng/#/login,https://nibsserpproc.nibss-plc.com.ng,https://budget.nibss-plc.com.ng";

        //string Origin = "https://nibsserpworkflow.nibss-plc.com.ng,https://10.7.7.147:43440";
        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            Configuration = config;
            Environment = env;

            AppSettings = new AppSettings();
            Configuration.Bind(AppSettings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //string[] s = Configuration[Origin].Split(",");
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "MyPolicy",
            //                      builder =>
            //                      {
            //                          builder.WithOrigins(s);
            //                      });
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("MyPolicy", builder => builder
            //   // .WithOrigins(Origin.Split(","))
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()); 

            //});

            services.AddCors();



            // New Security-Header (hsts)
            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
            #region snippet1
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSignalR();
            services.AddMvc()
                .AddViewLocalization(options => options.ResourcesPath = "Resources")
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //     .SetIsOriginAllowed((host) => true)
            //           .AllowAnyMethod()

            //           .AllowAnyHeader();
            //}));


            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                         new CultureInfo("fr"),
                        new CultureInfo("en-US")
                       
                        //new CultureInfo("ru"),
                        //new CultureInfo("ja"),
                        //new CultureInfo("fr-FR"),
                        //new CultureInfo("zh-CN"),
                        //new CultureInfo("ar-EG"),
                    };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            #endregion
            services.Configure<IISOptions>(options =>
            {

                options.ForwardClientCertificate = false;
                options.AutomaticAuthentication = false;
                options.AuthenticationDisplayName = "Windows";
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // const string connectionString = @"Data Source=fintraksqlmmbs.database.windows.net;Database=TheCoreBankingAzure;user id=fintrak;password=Password20$;";
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            });


            services.AddAuthorization(options =>
            {
                // inline policies
                options.AddPolicy("user", policy =>
                {

                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("user", "finance");
                    policy.RequireClaim("User", "Register");
                    // policy.RequireRole("user", "finance");

                });
                options.AddPolicy("user", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Setup");
                });
            });

            var connectionString = Configuration.GetConnectionString("TheCoreBanking");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));



            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                //previous code removed for clarity reasons

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            })
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMvc(options => options.Filters
             .Add(new AutoValidateAntiforgeryTokenAttribute()))
                 .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            //services.AddMvc();
            //services.AddSingleton<ICorsPolicyService, CorsPolicyService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<IMISRepository, MISRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IRankRepository, RankRepository>();
            services.AddScoped<ISetupUnitOfWork, SetupUnitOfWork>();
            services.AddScoped<IRepositoryProvider, RepositoryProvider>();
            services.AddSingleton<RepositoryFactories>();
            services.AddScoped<IAuthenticateUnitOfWork, AuthenticateUnitOfWork>();
            services.AddScoped<IRepositoryProvider2, RepositoryProvider2>();
            services.AddSingleton<RepositoryFactories2>();
            services.AddSingleton<ICorsPolicyService>((container) =>
            {
                var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
                return new DefaultCorsPolicyService(logger)
                {
                    //AllowedOrigins = Origin.Split(",")
                    AllowAll= true

                    
                };
            });
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiForgeryFieldName";
                options.HeaderName = "AntiForgeryHeaderName";
                options.Cookie.Name = "AntiForgeryCookieName";
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromHours(1);
                o.SlidingExpiration = true;
                o.AccessDeniedPath = "/Account/AccessDenied";
                o.LoginPath = "/Account/login";
                o.LogoutPath = "/Account/logout";
                o.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
                o.Cookie.SameSite = SameSiteMode.Strict;
                o.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            });

            var identityServer = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;


                //options.Authentication = new AuthenticationOptions()
                //{
                //    CookieLifetime = TimeSpan.FromMinutes(5), // ID server cookie timeout set to 10 hours
                //    CookieSlidingExpiration = true

                //};
            })
            .AddAspNetIdentity<ApplicationUser>()

            // this adds the config data from DB (clients, resources, CORS)
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            // this adds the operational data from DB (codes, tokens, consents)
           .AddOperationalStore(options =>
           {
               options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
               // this enables automatic token cleanup. this is optional.
               options.EnableTokenCleanup = true;
               options.TokenCleanupInterval = 65; // interval in seconds. 15 seconds useful for debugging
           });


            var getIssuer = Configuration.GetSection("SigninKeyCredentials").GetValue<string>("KeyStoreIssuer");


            if (Environment.IsDevelopment())
            {


                identityServer.AddDeveloperSigningCredential();

            }
            else
            {

                //var fileName = Path.Combine(Environment.ContentRootPath, "certificate.pfx");

                //if (!File.Exists(fileName))
                //{
                //    throw new FileNotFoundException("Signing Certificate is missing!");
                //}

                //var cert = new X509Certificate2(fileName, "fintr@k");
                // identityServer.AddSigningCredential(new X509Certificate2(fileName, "fintr@k", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.EphemeralKeySet));
                //identityServer.AddSigningCredential(cert);
                identityServer.AddDeveloperSigningCredential();

            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // New security-header (hsts)
                app.UseHsts();
                //   app.UseCsp(options => options
                //   .UpgradeInsecureRequests()
                //.DefaultSources(s => s.Self())
                //.ScriptSources(s => s.Self())
                //.StyleSources(s => s.Self())
                //.MediaSources(s => s.Self())
                //);
                //app.Use(async (context, next) =>
                //{
                //    //this is for the request header authentication
                //    context.Response.Headers.Add(
                //"Content-Security-Policy",
                //"script-src 'self'; " +
                //"style-src 'self'; " +
                //"img-src 'self'");
                //    context.Request.Headers.Add("X-Content-Type-Options", "nosniff");
                //    context.Request.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                //    context.Request.Headers.Add("Strict-Transport-Security", "max-age=63072000; includeSubDomains; preload");
                //    context.Request.Headers.Add("X-XSS-Protection", "1; mode=block");

                //    //this is the response headers authentication
                //    // context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                //    //// context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                //    // context.Response.Headers.Add("Set-Cookie", "HttpOnly; Secure;" + SameSiteMode.Strict + ";" + CookieSecurePolicy.Always);
                //    // context.Response.Headers.Add("Strict-Transport-Security", "max-age=63072000; includeSubDomains; preload");
                //    // context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                //    await next();
                //});
            }
            //if (Environment.IsProduction())
            //{
                app.UseDeveloperExceptionPage();

                
            //}
            //else
            //{
            // app.UseExceptionHandler("/Home/Error");
            // }


            app.UseHttpsRedirection();
            //New Security 
            app.UseAntiXssMiddleware();
            app.UseIdentityServer();
           
            app.UseReferrerPolicy(opts => opts.NoReferrer());

            app.UseSecurityHeaders(AppSettings.SecurityHeaders);
            app.UseStaticFiles();

            //app.UseCors(options =>
            //    options.AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod().SetIsOriginAllowed(x =>true)
            //);

         

            //app.Use(async (context, next) =>
            //{
            //    //this is the response headers authentication
            //    context.Response.Headers.Add(
            //   "Content-Security-Policy",
            //   "default-src 'none';" +
            //        "script-src 'self' 'unsafe-inline';'unsafe-eval';" +
            //        "script-src-elem 'self' 'unsafe-inline';" +
            //        "connect-src 'self';" +
            //        "img-src 'self';" +
            //        "style-src 'self' 'unsafe-inline';" +
            //        "style-src-elem 'self' 'unsafe-inline';" +
            //        "font-src 'self';" +
            //        "media-src 'self';" +
            //        "frame-src 'self';");
            //    context.Response.Headers.Add("access-control-allow-origin", "sameorigin");
            //    context.Response.Headers.Add("x-content-type-options", "nosniff");
            //    context.Response.Headers.Add("set-cookie", "httponly; secure;" + SameSiteMode.Strict + ";" + CookieSecurePolicy.Always);
            //    context.Response.Headers.Add("x-frame-options", "sameorigin");
            //    context.Response.Headers.Add("strict-transport-security", "max-age=63072000; includesubdomains; preload");
            //    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            //    await next();
            //});

            app.UseCors(builder => builder.WithOrigins(Origin.Split(",")).AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();
           
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });



        }





    }
}
