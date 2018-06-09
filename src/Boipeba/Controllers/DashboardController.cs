using System.Web.Mvc;
using Boipeba.Core.Domain.Controller;

namespace Boipeba.Controllers
{
    public class DashboardController: Controller, IBoipebaController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}