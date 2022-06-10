﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace TheCoreBanking.Authenticate
{
    /// <summary>
    /// Impl of adding a signin key for identity server 4,
    /// with an appsetting.json configuration look similar to:
    /// "SigninKeyCredentials": {
    ///     "KeyType": "KeyFile",
    ///     "KeyFilePath": "C:\\certificates\\idsv4.pfx",
    ///     "KeyStorePath": ""
    /// }
    /// </summary>
    public static class SigninCredentialExtension
    {
        private const string KeyType = "KeyType";
        private const string KeyTypeKeyFile = "KeyFile";
        private const string KeyTypeKeyStore = "KeyStore";
        private const string KeyTypeTemporary = "Temporary";
        private const string KeyFilePath = "KeyFilePath";
        private const string KeyFilePassword = "KeyFilePassword";
        private const string KeyStoreIssuer = "KeyStoreIssuer";

        public static IIdentityServerBuilder AddSigninCredentialFromConfig(
            this IIdentityServerBuilder builder, IConfigurationSection options)
        {
            string keyType = options.GetValue<string>(KeyType);
            //logger.LogDebug($"SigninCredentialExtension keyType is {keyType}");

            switch (keyType)
            {
                case KeyTypeTemporary:
                  //  logger.LogDebug($"SigninCredentialExtension adding Temporary Signing Credential");
                    builder.AddDeveloperSigningCredential();
                    break;

                case KeyTypeKeyFile:
                    AddCertificateFromFile(builder, options);
                    break;

                case KeyTypeKeyStore:
                    AddCertificateFromStore(builder, options);
                    break;
            }

            return builder;
        }

        private static void AddCertificateFromStore(IIdentityServerBuilder builder,
            IConfigurationSection options)
        {
            var keyIssuer = options.GetValue<string>(KeyStoreIssuer);
            //logger.LogDebug($"SigninCredentialExtension adding key from store by {keyIssuer}");

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, keyIssuer, true);

            if (certificates.Count > 0)
                builder.AddSigningCredential(certificates[0]);
           // else
                builder.AddDeveloperSigningCredential();
            // logger.LogError("A matching key couldn't be found in the store");
        }

        private static void AddCertificateFromFile(IIdentityServerBuilder builder,
            IConfigurationSection options)
        {
            var keyFilePath = options.GetValue<string>(KeyFilePath);
            var keyFilePassword = options.GetValue<string>(KeyFilePassword);

            if (File.Exists(keyFilePath))
            {
                
               // logger.LogDebug($"SigninCredentialExtension adding key from file {keyFilePath}");
                builder.AddSigningCredential(new X509Certificate2(keyFilePath, keyFilePassword));
            }
            else
            {
                //logger.LogError($"SigninCredentialExtension cannot find key file {keyFilePath}");
            }
        }
    }
}