using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos.Repositories;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class HistoricoController : BaseController
    {
        private readonly IProcessoRepository _processoRepository;

        public HistoricoController(IProcessoRepository processoRepository)
        {
            _processoRepository = processoRepository;
        }

        [HttpPost]
        public ActionResult Index(long idHistorico)
        {
            return View(idHistorico);
        }

        public JsonResult GetViewData(long idProcesso)
        {
            return Json(_processoRepository.HistoricoProcesso(idProcesso));
        }
    }
}