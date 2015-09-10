using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using Kentor.AuthServices;
using Kentor.AuthServices.Configuration;
using Kentor.AuthServices.Owin;
using Kentor.AuthServices.WebSso;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Auth
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get());

            var cors = new InMemoryCorsPolicyService(Clients.Get());
            factory.CorsPolicyService = new Registration<ICorsPolicyService>(cors);

            var spOptions = new SPOptions
            {
                EntityId = new EntityId("http://localhost:13856/AuthServices")
            };


            var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var path = new Uri(uriPath).LocalPath;
            var authOptions = new KentorAuthServicesAuthenticationOptions(false)
            {
                SPOptions = spOptions,
                AuthenticationType = "saml2p",
                Caption = "SAML2p",
            };
            authOptions.IdentityProviders.Add(
                new IdentityProvider(
                    new EntityId("http://stubidp.kentor.se/Metadata"), spOptions)
                {
                    AllowUnsolicitedAuthnResponse = true,
                    Binding = Saml2BindingType.HttpRedirect,
                    SingleSignOnServiceUrl = new Uri("http://stubidp.kentor.se"),
                    SigningKey = new X509Certificate2(path + "/Kentor.AuthServices.StubIdp.cer").PublicKey.Key
                });

            var options = new IdentityServerOptions
            {
                Factory = factory,
                RequireSsl = false,
                AuthenticationOptions = new AuthenticationOptions()
                {
                    IdentityProviders = (appBuilder, signInAsType) =>
                    {
                        Kentor.AuthServices.Configuration.Options.GlobalEnableSha256XmlSignatures();
                        authOptions.SignInAsAuthenticationType = signInAsType;
                        app.UseKentorAuthServicesAuthentication(authOptions);
                    },
                    EnableLocalLogin = false
                },
                LoggingOptions = new LoggingOptions()
                {
                    EnableHttpLogging = true,
                    EnableKatanaLogging = true,
                    EnableWebApiDiagnostics = true
                }
            };
            app.UseIdentityServer(options);
        }
    }
}
