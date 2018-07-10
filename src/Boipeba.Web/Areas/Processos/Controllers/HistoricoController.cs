using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;
using Boipeba.Web.Controllers;
using NHibernate.Util;

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
            var listaMovimentos = _processoRepository.HistoricoProcesso(idProcesso);

            return Json(new
            {
                Movimentos = listaMovimentos,

                Processo = listaMovimentos.Count > 0 ? listaMovimentos.First().Processo : null
            });
        }
    }
}