using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using Thinktecture.IdentityModel.Clients;

namespace JWTRequestExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // ONLY IDIOTS DO THAT ON PRODUCTION
            DisableCertificateValidation();

            var token = RequestToken();

            Console.WriteLine(token);

            callService(token);

            Console.ReadKey();

        }

        private static void callService(string token)
        {
            var client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:44308")
                };

            client.SetBearerToken(token);

            var response = client.GetAsync("Home/Get").Result;

            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync().Result;

        }

        private static string RequestToken()
        {
            var client = new OAuth2Client(new Uri("https://localhost/idsrv/issue/oauth2/token"), "OAuthTest",
                                          "Zt7EAk32Sp2W5QorFC3DDGWSkp49bXYirFexRUmFrr4=");

            var response = client.RequestAccessTokenUserName("admin", "BASE64", "https://localhost:44308/");

            return response.AccessToken;
        }


        // DISABLE CERTIFICATI VALIDATION
        private static void DisableCertificateValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate
                { return true; });
        }
    }
}
