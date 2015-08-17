using IdentityServer3.Core.Models;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var response = GetClientToken().Result;
            Console.ReadKey();
        }
        static async Task<TokenResponse> GetClientToken()
        {
            using (var client = new HttpClient())
            {

                var content = new FormUrlEncodedContent(new[] 
                {
                    new KeyValuePair<string, string>("client_id", "silicon"),
                    new KeyValuePair<string, string>("client_secret", "F621F470-9731-4A25-80EF-67A6F7C5F4B8"),
                    new KeyValuePair<string, string>("scope", "api1 openid"),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });


                var response = await client.PostAsync("https://100.105.80.38:13855/connect/token", content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return new TokenResponse()
                    {
                        AccessToken = "",
                        AccessTokenLifetime = 0,
                        IdentityToken = "",
                        RefreshToken = ""
                    };
                }
                else
                {
                    throw new Exception("ERROR, status: " + response.StatusCode.ToString() + response.Content.ReadAsStringAsync().Result);
                }
            }
        }
    }

}
