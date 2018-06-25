using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Boipeba.Core.Domain.Model;
using Boipeba.Core.Modulos.Processos;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Web.API
{
    public class DestinatariosController : ApiController
    {
        private readonly IOrgaoUnidadeRepository _orgaoUnidadeRepository;
        private readonly IPessoaRepository _pessoaRepository;

        public DestinatariosController(IOrgaoUnidadeRepository orgaoUnidadeRepository, IPessoaRepository pessoaRepository)
        {
            _orgaoUnidadeRepository = orgaoUnidadeRepository;
            _pessoaRepository = pessoaRepository;
        }

        public IList<IdentifiableDescriptionItem> Get(string part)
        {
            var list = new List<IdentifiableDescriptionItem>();

            list.AddRange(_orgaoUnidadeRepository.Find(part).Select(x => new IdentifiableDescriptionItem
            {
                Id = x.IdOrgaoUnidade,
                Descricao = x.DsOrgaoUnidade,
                Tipo = x.GetType().Name
            }));

            list.AddRange(_pessoaRepository.Find(part, true).Select(x => new IdentifiableDescriptionItem
            {
                Id = x.Matricula,
                Descricao = x.Nome,
                Tipo = x.GetType().Name
            }));

            return list.Take(20).ToList();
        }
    }
}
