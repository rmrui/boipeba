using System.Web.Mvc;

namespace Boipeba.Web.Controllers
{
    public class TramitacaoController: BaseController
    {
        public ActionResult SolicitarDiaria()
        {
            return View("Diaria/SolicitarDiaria");
        }
    }
}