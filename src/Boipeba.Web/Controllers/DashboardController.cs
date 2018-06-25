using System.Collections.Generic;
using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;
using Boipeba.Core.Modulos.Processos.Services;

namespace Boipeba.Web.Controllers
{
    public class DashboardController: BaseController
    {
        private readonly IProcessoRepository _processoRepository;

        public DashboardController(IProcessoRepository processoRepository)
        {
            _processoRepository = processoRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetViewData()
        {
            var list = _processoRepository.AtribuidosPara(Usuario.Matricula);

            //var list = new List<Processo>
            //{
            //    new Processo
            //    {
            //        Assunto = new Assunto {DsAssunto = "teste1"},
            //        Simp = "123",
            //        PessoaDestino = new Pessoa {Nome = "Rui"}
            //    },
            //    new Processo
            //    {
            //        Assunto = new Assunto {DsAssunto = "teste2"},
            //        Simp = "456",
            //        PessoaDestino = new Pessoa {Nome = "Rui"}
            //    }
            //};

            return Json(new {Processos = list});
        }
    }
}