using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Web.API
{
    public class AssuntosController : ApiController
    {
        private readonly IAssuntoRepository _repository;

        public AssuntosController(IAssuntoRepository repository)
        {
            _repository = repository;
        }

        public IList<Assunto> Get(string part)
        {
            return _repository.Find(part).Take(20).ToList();
        }
    }
}