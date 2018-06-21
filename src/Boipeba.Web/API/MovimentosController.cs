using System.Collections.Generic;
using System.Web.Http;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Web.API
{
    public class MovimentosController : ApiController
    {
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentosController(IMovimentoRepository movimentoRepository)
        {
            _movimentoRepository = movimentoRepository;
        }

        public IList<Movimento> Find(string part)
        {
            return _movimentoRepository.Find(part);
        }
    }
}