﻿namespace Nancy.Authenticaton.Forms.Owin.NancyApp
{
    using global::Owin;
    using Nancy.Authentication.Forms;
    using Nancy.Cryptography;

    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var userManager = new UserManager();
            var cryptographyConfiguration = new CryptographyConfiguration(
                new RijndaelEncryptionProvider(new PassphraseKeyGenerator("SuperSecretPass", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 1000)),
                new DefaultHmacProvider(new PassphraseKeyGenerator("UberSuperSecure", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 1000)));
            var formsAuthenticationConfiguration = new FormsAuthenticationConfiguration
                                                   {
                                                       RedirectUrl = "~/login",
                                                       DisableRedirect = true,
                                                       UserMapper = userManager,
                                                       CryptographyConfiguration = cryptographyConfiguration
                                                   };
            builder
                .UseNancyAuth(formsAuthenticationConfiguration, userManager)
                .UseNancy(opt => opt.Bootstrapper = new AppBootstrapper(formsAuthenticationConfiguration, userManager));
        }
    }
}