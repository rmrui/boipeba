using System.Web.Mvc;
using Boipeba.Core;

namespace Boipeba.Web.Controllers
{
    public class LabController : BaseController
    {
        public LabController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}