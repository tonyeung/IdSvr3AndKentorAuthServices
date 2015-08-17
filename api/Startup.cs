using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.AccessTokenValidation;
using Owin;
using System.Web.Http;

namespace api
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            // accept access tokens from identityserver and require a scope of 'api1'
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = "https://100.105.80.38:13855",
                    ValidationMode = ValidationMode.ValidationEndpoint,

                    RequiredScopes = new[] { "api1" }
                });

            // configure web api
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // require authentication for all controllers
            config.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(config);
        }

    }
}