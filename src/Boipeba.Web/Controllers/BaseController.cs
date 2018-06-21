using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Boipeba.Core.Auth;
using Boipeba.Core.Domain.Controller;

namespace Boipeba.Web.Controllers
{
    public class BaseController: Controller, IBoipebaController
    {
        public Usuario Usuario => (Usuario) User;

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