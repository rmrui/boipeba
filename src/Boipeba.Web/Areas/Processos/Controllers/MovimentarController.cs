using System;
using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Exceptions;
using Boipeba.Core.Modulos.Processos.Repositories;
using Boipeba.Core.Modulos.Processos.Services;
using Boipeba.Web.Controllers;

namespace Boipeba.Web.Areas.Processos.Controllers
{
    public class MovimentarController : BaseController
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IProcessoService _processoService;

        public MovimentarController(IProcessoRepository processoRepository, IProcessoService processoService)
        {
            _processoRepository = processoRepository;
            _processoService = processoService;
        }

        [HttpPost]
        public ActionResult Index(long id)
        {
            var processo = _processoRepository.Find(id);

            return View(processo);
        }

        public JsonResult Salvar(ProcessoMovimento processoMovimento)
        {
            try
            {
                processoMovimento.Autor = new Pessoa { Id = Usuario.Matricula };
                _processoService.Movimentar(processoMovimento);
            }
            catch (MovimentoRepetidoException ex)
            {
                return JsonError("Movimento repetido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return Done();
        }
    }
}