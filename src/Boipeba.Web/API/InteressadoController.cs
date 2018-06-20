using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Boipeba.Core.Domain.Model;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Web.API
{
    public class InteressadoController : ApiController
    {
        private readonly IOrgaoUnidadeRepository _repository;
        private readonly IPessoaRepository _pessoaRepository;

        public InteressadoController(IOrgaoUnidadeRepository repository, IPessoaRepository pessoaRepository)
        {
            _repository = repository;
            _pessoaRepository = pessoaRepository;
        }

        public IList<IdentifiableDescriptionItem> Get(string part)
        {
            var list = new List<IdentifiableDescriptionItem>();

            list.AddRange(_repository.Find(part).Select(x => new IdentifiableDescriptionItem
            {
                Id = x.IdOrgaoUnidade,
                Descricao = x.DsOrgaoUnidade
            }));

            list.AddRange(_pessoaRepository.Find(part).Select(x => new IdentifiableDescriptionItem
            {
                Id = x.Matricula,
                Descricao = x.Nome
            }));

            return list.ToList();
        }
    }
}
