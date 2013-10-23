using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Util;
using WebHost.Security;

namespace WebHost
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // claims transformation
        protected void Application_PostAuthenticateRequest()
        {
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var principal = new ClaimsTransformer().Authenticate(string.Empty, ClaimsPrincipal.Current);
                System.Web.HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }
    }
}