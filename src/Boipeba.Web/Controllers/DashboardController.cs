using System.Web.Mvc;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Web.Controllers
{
    public class DashboardController: BaseController
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly ITicketProvider _ticketProvider;
        private readonly IPessoaRepository _pessoaRepository;

        public DashboardController(IProcessoRepository processoRepository, ITicketProvider ticketProvider, IPessoaRepository pessoaRepository)
        {
            _processoRepository = processoRepository;
            _ticketProvider = ticketProvider;
            _pessoaRepository = pessoaRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetViewData()
        {
            var pessoa = _pessoaRepository.Find(_ticketProvider.ResolveUser().Matricula);
            return Json(_processoRepository.GetMailBox(pessoa));
        }
    }
}