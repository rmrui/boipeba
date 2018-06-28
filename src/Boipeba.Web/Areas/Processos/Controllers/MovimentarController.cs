using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class MovimentarController : BaseController
    {
        // /Processos/Movimentar/1
        public ActionResult Index(int id)
        {
            return View();
        }

        public JsonResult Salvar(Movimento movimento)
        {
            return Done();
        }
    }
}