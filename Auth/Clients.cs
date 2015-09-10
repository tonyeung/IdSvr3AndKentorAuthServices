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
                    ClientName = "Authorization Code Grant",
                    ClientId = "authCode",
                    AccessTokenType = AccessTokenType.Reference,
                    AllowAccessToAllScopes = true,
                    Flow = Flows.AuthorizationCode,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:6463/token/callback"
                    }
                },
                new Client
                {
                    ClientName = "Client Credentials Grant",
                    ClientId = "clientCreds",
                    AccessTokenType = AccessTokenType.Reference,
                    AllowAccessToAllScopes = true,
                    Flow = Flows.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    }
                },
                new Client
                {
                    ClientName = "password grant",
                    ClientId = "password",
                    AccessTokenType = AccessTokenType.Reference,
                    AllowAccessToAllScopes = true,
                    Flow = Flows.ResourceOwner,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    }
                },
                new Client
                {
                    ClientName = "Katana Hybrid Client Demo",
                    ClientId = "hybridWeb",
                    AccessTokenType = AccessTokenType.Reference, 
                    AllowAccessToAllScopes = true,
                    Flow = Flows.Hybrid,
                    ClientSecrets = new List<Secret>
                    { 
                        new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    },           
                    RedirectUris = new List<string>
                    {
                        "http://localhost:6463/token/callback",
                    }
                }

            };
        }
    }
}
