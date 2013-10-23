using System.Web.Http;
using Thinktecture.IdentityModel.Tokens.Http;
using WebHost.Security;

namespace WebHost
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var authentication = CreateAuthenticationConfiguration();

            config.MessageHandlers.Add(new AuthenticationHandler(authentication));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }

        public static AuthenticationConfiguration CreateAuthenticationConfiguration()
        {
            var authentication = new AuthenticationConfiguration()
                {
                    ClaimsAuthenticationManager = new ClaimsTransformer(),
                    RequireSsl = false,
                    EnableSessionToken = true
                };

            authentication.AddJsonWebToken(
                issuer: "http://identityserver.v2.thinktecture.com/trust/idsrv",
                audience: "https://localhost:44308/",
                signingKey: "CBvAWq7BA9EncagGwAK2gTrEhs2IL20LiHIhFtxRIT4=");

            return authentication;
        }
    }
}