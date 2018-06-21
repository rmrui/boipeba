using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public interface IProcessoRepository : ICrudRepository<Processo, long>
    {
        IList<Processo> GetMailBox(Pessoa responsavel);
    }

    public class ProcessoRepository : DefaultRepository<Processo, long>, IProcessoRepository
    {
        public ProcessoRepository(ISession session) : base(session)
        {
        }

        public IList<Processo> GetMailBox(Pessoa responsavel)
        {
            var condicoes = Restrictions.Disjunction();
            Processo alvo = null;
            condicoes.Add(Restrictions.Eq(Projections.Property(() => alvo.PessoaDestino), responsavel));
            condicoes.Add(Restrictions.Eq(Projections.Property(() => alvo.OrgaoUnidadeDestino), responsavel.OrgaoUnidadeLotacao));

            return Session.QueryOver(() => alvo).Where(condicoes).OrderBy(x => x.UltimaAlteracao).Desc.List();
        }
    }
}