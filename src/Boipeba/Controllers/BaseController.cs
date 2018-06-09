using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Boipeba.Core.Domain.Controller;

namespace Boipeba.Controllers
{
    public class BaseController: Controller, IBoipebaController
    {
        public JsonResult Done()
        {
            return Json(new { }, "text/html");
        }

        public JsonResult JsonError(string msg = "Serviço indisponível")
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return Json(new {message = msg});
        }
    }
}