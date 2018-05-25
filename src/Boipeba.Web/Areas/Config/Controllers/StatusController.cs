using System.Web.Mvc;
using Boipeba.Core.Modulos.Cadastro;
using Boipeba.Core.Modulos.Cadastro.Repositories;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Config.Controllers
{
   // [AuthorizeRoles("SuperAdmin", "Admin")]
    public class StatusController: BaseController
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetViewData()
        {
            return Json(_statusRepository.FindAll());
        }

        public JsonResult Salvar(Status status)
        {
            _statusRepository.AddOrUpdate(status);

            return Done();
        }
    }
}