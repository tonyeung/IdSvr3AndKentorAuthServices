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
            var response = GetClientToken();
            CallApi(response);
            Console.ReadKey();
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
