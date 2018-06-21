using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public interface IProcessoRepository : ICrudRepository<Processo, long>
    {
        IList<Processo> AtribuidosPara(int matricula);
    }

    public class ProcessoRepository : DefaultRepository<Processo, long>, IProcessoRepository
    {
        public ProcessoRepository(ISession session) : base(session)
        {

        }

        //TODO: Incluir atribuidos ao Orgao Unidade da pessoa.
        //TODO: Onde consultar qual orgao unidade a pessoa esta associada?
        public IList<Processo> AtribuidosPara(int matricula)
        {
            return Session.QueryOver<Processo>()
                .Where(x => x.PessoaDestino.Matricula == matricula)
                .List();
        }
    }
}