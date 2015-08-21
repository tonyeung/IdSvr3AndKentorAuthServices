using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using Kentor.AuthServices;
using Kentor.AuthServices.Configuration;
using Kentor.AuthServices.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Auth
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var authServicesOptions = new KentorAuthServicesAuthenticationOptions(false)
            {
                SPOptions = new SPOptions
                {
                    EntityId = new EntityId("http://sp.example.com")
                },

                SignInAsAuthenticationType = "External",
                AuthenticationType = "saml2p",
                Caption = "SAML2p"                
            };
            authServicesOptions.IdentityProviders.Add(new IdentityProvider(new EntityId("http://stubidp.kentor.se/Metadata"), authServicesOptions.SPOptions)
            {
                LoadMetadata = true
            });


            var cors = new InMemoryCorsPolicyService(Clients.Get());
            var factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(new List<InMemoryUser>());

            factory.CorsPolicyService = new Registration<ICorsPolicyService>(cors);

            var options = new IdentityServerOptions
            {
                Factory = factory,
                AuthenticationOptions = new AuthenticationOptions()
                {
                    IdentityProviders = (appBuilder, signInAsType) =>
                    {
                        Kentor.AuthServices.Configuration.Options.GlobalEnableSha256XmlSignatures();
                        app.UseKentorAuthServicesAuthentication(authServicesOptions);
                    }
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
