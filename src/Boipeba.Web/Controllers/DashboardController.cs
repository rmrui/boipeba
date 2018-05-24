using System.Web.Mvc;

namespace Boipeba.Web.Controllers
{
    public class DashboardController: BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}