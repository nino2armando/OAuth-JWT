using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Extensions;
using WebHost.Security;

namespace WebHost.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ViewClaims Get()
        {
            var request = Request.ToHttpRequestMessage();
            var principal = request.GetClaimsPrincipal();
            return ViewClaims.GetAll(principal);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
