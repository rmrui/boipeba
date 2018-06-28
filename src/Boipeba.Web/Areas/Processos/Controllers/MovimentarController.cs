using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class MovimentarController : BaseController
    {
        private readonly IProcessoRepository _processoRepository;

        public MovimentarController(IProcessoRepository processoRepository)
        {
            _processoRepository = processoRepository;
        }

        [HttpPost]
        public ActionResult Index(long id)
        {
            var processo = _processoRepository.Find(id);

            return View(processo);
        }

        public JsonResult Salvar(ProcessoMovimento movimento)
        {
            return Done();
        }
    }
}