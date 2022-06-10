// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
//using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;

using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;

namespace TheCoreBanking.Authenticate
{
    public static class Config
    {

        public class CertificateConfiguration
        {
            public bool UseLocalCertStore { get; set; }
            public string CertificateThumbprint { get; set; }
            public string CertificateNameKeyVault { get; set; }
            public string KeyVaultEndpoint { get; set; }
            public string DevelopmentCertificatePfx { get; set; }
            public string DevelopmentCertificatePassword { get; set; }
        }

      

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("roles", "Your Roles", new List<string>())

                // new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location" })
            };
        }
       
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
              
                new Client {
                    ClientId = "TheCoreBanking.Main",
                    ClientName = "The Core Banking",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string> {"https://localhost:44339/signin-oidc"},
                     //{"http://localhost:21812/signin-oidc"}
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    },
                    ClientSecrets = {new Secret("secret".Sha256())},
                    FrontChannelLogoutUri ="https://localhost:44339/signout-oidc",
                    PostLogoutRedirectUris = new List<string> { "https://localhost:44339/signout-oidc-callback-oidc" }
                },
                  new Client {
                    ClientId = "TheCoreBanking.Products",
                    ClientName = "The Core Banking Products",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string> {"https://fintrakbankingmmbs-Product.azurewebsites.net/signout-oidc"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    },
                    ClientSecrets = {new Secret("secret".Sha256())},
                    FrontChannelLogoutUri ="https://fintrakbankingmmbs-Product.azurewebsites.net/signout-oidc",
                    PostLogoutRedirectUris = new List<string> { "https://fintrakbankingmmbs-Product.azurewebsites.net/signout-callback-oidc" }
                },
                new Client {
                    ClientId = "TheCoreBanking.Customers",
                    ClientName = "The Core Banking Customers",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string> {"http://localhost:1659/signin-oidc"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    },
                    ClientSecrets = {new Secret("secret".Sha256())},
                    FrontChannelLogoutUri ="http://localhost:1659/signout-oidc",
                    PostLogoutRedirectUris = new List<string> { "http://localhost:1659/signout-callback-oidc" }
                },
                   new Client {
                    ClientId = "TheCoreBanking.Finance",
                    ClientName = "The Core Banking Finance",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>{ "https://localhost:44379/signin-oidc" }, //{"https://localhost:44326/signin-oidc"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    },
                    ClientSecrets = {new Secret("secret".Sha256())},
                    FrontChannelLogoutUri ="https://localhost:44379/signout-oidc",
                    PostLogoutRedirectUris = new List<string> { "https://localhost:44379/signout-callback-oidc" }
                }

            };
        }


  

    }
}
