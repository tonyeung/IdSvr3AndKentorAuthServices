﻿using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auth
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
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
                    EnableLocalLogin = false
                },
                //AuthenticationOptions = new AuthenticationOptions()
                //{
                //    IdentityProviders = (appBuilder, signInAsType) =>
                //    {
                //        Kentor.AuthServices.Configuration.Options.GlobalEnableSha256XmlSignatures();
                //        app.UseKentorAuthServicesAuthentication(new KentorAuthServicesAuthenticationOptions(true));
                //    }
                //},
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
