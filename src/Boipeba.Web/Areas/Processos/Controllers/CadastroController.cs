using System.Web.Mvc;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class CadastroController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Generico()
        {
            return View();
        }
    }
}