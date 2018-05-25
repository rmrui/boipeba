using System.Web.Mvc;
using Boipeba.Core.Modulos.Cadastro;
using Boipeba.Core.Modulos.Cadastro.Repositories;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Config.Controllers
{
    public class CasinhaController: BaseController
    {
        private readonly ICasinhaRepository _casinhaRepository;

        public CasinhaController(ICasinhaRepository casinhaRepository)
        {
            _casinhaRepository = casinhaRepository;
        }

        public JsonResult GetViewData()
        {
            return Json(_casinhaRepository.FindAll());
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Salvar(Casinha casinha)
        {
            _casinhaRepository.AddOrUpdate(casinha);

            return Done();
        }

        public JsonResult Excluir(Casinha casinha)
        {
            _casinhaRepository.Remove(casinha);

            return Done();
        }
    }
}