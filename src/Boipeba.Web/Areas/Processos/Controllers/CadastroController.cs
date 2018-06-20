using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class CadastroController : BaseController
    {
        private readonly IProcessoRepository _processoRepository;

        public CadastroController(IProcessoRepository processoRepository)
        {
            _processoRepository = processoRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Generico()
        {
            return View();
        }

        public JsonResult Salvar(Processo processo)
        {
            _processoRepository.AddOrUpdate(processo);

            return Done();
        }
    }
}