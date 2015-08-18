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
            //var response = GetClientToken();
            //CallApi(response);
            Authenticate();
            Console.ReadKey();
        }

        static void Authenticate()
        {
            //var client = new HttpClient();
            var url = "https://100.105.80.38:13855/connect/authorize?client_id=hybridclient&redirect_uri=https://100.105.80.38:13855/api/ExternalLoginCallback/&response_type=code id_token token&scope=openid profile read write offline_access&state=14395882522910.8145003772806376&nonce=1646515461546";
            Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe", "-private-window \"https://100.105.80.38:13855/connect/authorize?client_id=hybridclient&redirect_uri=https://100.105.80.38:13855/api/ExternalLoginCallback/&response_type=code id_token token&scope=openid profile read write offline_access&state=14395882522910.8145003772806376&nonce=1646515461546\"");
            //Console.WriteLine(client.GetStringAsync(url).Result);
        }


        static TokenResponse GetClientToken()
        {
            var client = new TokenClient(
                "https://100.105.80.38:13855/connect/token",
                "silicon",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
            return client.RequestClientCredentialsAsync("api1").Result;
        }
        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            Console.WriteLine(client.GetStringAsync("http://localhost:10084/test").Result);
        }
    }

}
