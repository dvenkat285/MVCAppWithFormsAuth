using System.Web.Mvc;
using MVCAppWithFormsAuth.Filters;

namespace MVCAppWithFormsAuth.Controllers
{
    [AuthenticateFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}