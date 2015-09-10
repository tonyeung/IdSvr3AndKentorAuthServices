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
            // 10084
            // accept access tokens from identityserver and require a scope of 'api1'
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = "http://localhost:13855",
                    ValidationMode = ValidationMode.ValidationEndpoint
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