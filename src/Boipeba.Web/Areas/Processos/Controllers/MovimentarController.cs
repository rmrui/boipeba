using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class MovimentarController : BaseController
    {
        // GET
        public ActionResult Index(int id)
        {
            return View("../Cadastro/Movimentar");
        }

        public JsonResult Salvar(Movimento movimento)
        {
            return Done();
        }
    }
}