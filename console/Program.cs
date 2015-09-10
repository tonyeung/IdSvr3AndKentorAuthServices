using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Web;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(1000);
            //ResourceOwnerPassword();
            //ClientCredentials();
            //AuthorizationCode();
            Hybrid();
            //CallApi(response);
            Console.ReadKey();
        }

        static void Hybrid()
        {
            var nameValuePair = HttpUtility.ParseQueryString(string.Empty);

            nameValuePair["client_id"] = "hybridWeb";
            nameValuePair["scope"] = "offline_access openid";
            nameValuePair["response_type"] = "code token";
            //nameValuePair["redirect_uri"] = "http://localhost:6463/token/callback";
            nameValuePair["redirect_uri"] = "http://localhost:8888/login";
            nameValuePair["state"] = Guid.NewGuid().ToString("n");
            nameValuePair["nonce"] = Guid.NewGuid().ToString("n");

            var url = "http://localhost:13856/connect/authorize?" + nameValuePair.ToString();

            Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe", "-private-window \"" + url + "\"");
            //Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "-incognito --app=\"" + url + "\"");
        }

        static void AuthorizationCode()
        {
            var authorizeClient = new IdentityModel.Client.AuthorizeRequest("http://localhost:13856/connect/authorize");
            var url = authorizeClient.CreateAuthorizeUrl
            (
                clientId: "authCode",
                responseType: "code",
                scope: "offline_access",
                redirectUri: "http://localhost:6463/token/callback",
                state: Guid.NewGuid().ToString("n"),
                nonce: Guid.NewGuid().ToString("n")
            );

            Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe", "-private-window \"" + url + "\"");
        }

        static void ResourceOwnerPassword()
        {
            var client = new TokenClient(
                "http://localhost:13856/connect/token",
                "password",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
            var result = client.RequestResourceOwnerPasswordAsync("bob", "secret", "offline_access").Result;
        }

        static void ClientCredentials()
        {
            var client = new TokenClient(
                "http://localhost:13856/connect/token",
                "clientCreds",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
            var result = client.RequestClientCredentialsAsync("foo").Result;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            Console.WriteLine(client.GetStringAsync("http://localhost:13855/test").Result);
        }
    }

}
