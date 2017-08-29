using System.Web.Mvc;

namespace SimpleVoter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}