using System.Web.Mvc;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class MovimentarController : BaseController
    {
        // GET
        public ActionResult Index(string id)
        {
            return View("../Cadastro/Movimentar");
        }
    }
}