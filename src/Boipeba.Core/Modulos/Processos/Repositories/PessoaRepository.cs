using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public interface IPessoaRepository : IRepository
    {
        IList<Pessoa> Find(string partName, bool ativo);
        Pessoa Find(long id);
    }

    public class PessoaRepository : IPessoaRepository
    {
        protected readonly ISession Session;

        public PessoaRepository(ISession session)
        {
            Session = session;
        }

        public IList<Pessoa> Find(string partName, bool ativo)
        {
            return Session.QueryOver<Pessoa>().Where(x => x.Nome.IsLike(partName, MatchMode.Anywhere) && x.Ativo == ativo)
                .OrderBy(x => x.Nome).Asc.List();
        }

        public Pessoa Find(long id)
        {
            return Session.Get<Pessoa>(id);
        }
    }
}