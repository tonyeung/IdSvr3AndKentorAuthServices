using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    static class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Hybrid Client Demo",
                    Enabled = true,
                    ClientId = "hybridclient",
                    Flow = Flows.Hybrid,

                    RequireConsent = false,
                    AccessTokenType = AccessTokenType.Reference,
                    
                    AllowAccessToAllScopes = true,
                    AllowedCorsOrigins = new List<string>{"http://stubidp.kentor.se"},
                    
                    RedirectUris = new List<string>
                    {
                        "https://100.105.80.38:13855/api/ExternalLoginCallback/",
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://100.105.80.38:13855/hearbeat/"
                    }
                }

            };
        }
    }
}
