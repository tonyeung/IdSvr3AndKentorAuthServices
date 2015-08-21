using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    static class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "offline_access"
                },
                new Scope
                {
                    Name = "openid",
                    Type = ScopeType.Identity
                }
            };
        }
    }
}
