using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Services;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class CadastroController : BaseController
    {
        private readonly IProcessoService _processoService;

        public CadastroController(IProcessoService processoService)
        {
            _processoService = processoService;
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
            _processoService.Cadastrar(processo);

            return Done();
        }
    }
}