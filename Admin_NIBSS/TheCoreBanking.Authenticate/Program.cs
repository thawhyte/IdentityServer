// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.AspNetCore;
using Serilog.Events;
using System.Linq;
using NLog.Web;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace TheCoreBanking.Authenticate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "The Core Banking Identity";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(@"identityserver4_log.txt")
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            return WebHost.CreateDefaultBuilder(args)
                     .ConfigureAppConfiguration(builder =>
                     {
                         var root = builder.Build();
                         var vaultName = root["keyVault:Vault"];
                         builder.AddAzureKeyVault($"https://{vaultName}.vault.azure.net/",
                         root["keyVault:ClientId"], root["keyVault:Thumbprint"]);

     
                         
                     }).ConfigureKestrel(serverOptions =>
                     {
                         serverOptions.AddServerHeader = false;
                     })
                     .UseStartup<Startup>()
                    .UseNLog()
                    .ConfigureLogging(builder =>
                    {
                        builder.ClearProviders();
                        builder.AddSerilog();
                    })
                    .Build();
        }

        private static X509Certificate2 GetCertificate(string thumprint)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var certificateCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumprint, false);
                if (certificateCollection.Count == 0)
                {

                }
                return certificateCollection[0];

            }
            finally
            {

                store.Close();
            }
        }

    }
}