using System.Collections.Generic;
using System.Web.Http;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Web.API
{
    public class OrgaosUnidadesController : ApiController
    {
        private readonly IOrgaoUnidadeRepository _repository;

        public OrgaosUnidadesController(IOrgaoUnidadeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrgaoUnidade> Get()
        {
            return _repository.FindAll();
        }

        public OrgaoUnidade Get(int id)
        {
            return _repository.Find(id);
        }

        public IList<OrgaoUnidade> Get(string part)
        {
            return _repository.Find(part);
        }
    }
}
