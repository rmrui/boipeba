using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public interface IPessoaRepository : IRepository
    {
        IList<Pessoa> Find(string partName);
    }

    public class PessoaRepository : IPessoaRepository
    {
        protected readonly ISession Session;

        public PessoaRepository(ISession session)
        {
            Session = session;
        }

        public IList<Pessoa> Find(string partName)
        {
            return Session.QueryOver<Pessoa>().Where(x => x.Nome.IsLike(partName, MatchMode.Anywhere))
                .OrderBy(x => x.Nome).Asc.List();
        }
    }
}