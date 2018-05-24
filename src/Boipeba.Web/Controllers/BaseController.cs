using System.Web.Mvc;
using Boipeba.Core.Domain.Controller;

namespace Boipeba.Web.Controllers
{
    public class BaseController: Controller, IBoipebaController
    {
        public JsonResult Done()
        {
            return Json(new { }, "text/html");
        }
    }
}