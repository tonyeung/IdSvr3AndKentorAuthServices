using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(1000);
            var response = GetClientToken();
            //CallApi(response);
            //Authenticate();
            Console.ReadKey();
        }

        static void Authenticate()
        {

            var authorizeClient = new IdentityModel.Client.AuthorizeRequest("https://100.105.80.38:13855/connect/authorize");
            var url = authorizeClient.CreateAuthorizeUrl(
                clientId: "silicon",
                responseType: "code id_token token",
                scope: "openid offline_access",
                redirectUri: "http://localhost:10084/test",
                state: "adfasfadfa",
                nonce: "asdfasdfasdfasdfaf");


            Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe", "-private-window \"" + url + "\"");
            //Console.WriteLine(client.GetStringAsync(url).Result);
        }
        
        static TokenResponse GetClientToken()
        {
            var client = new TokenClient(
                "https://100.105.80.38:13856/connect/token",
                "silicon",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "email offline_access").Result;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            Console.WriteLine(client.GetStringAsync("https://100.105.80.38:13855/test").Result);
        }
    }

}
