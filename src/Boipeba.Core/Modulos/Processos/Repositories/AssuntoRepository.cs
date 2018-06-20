using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public class AssuntoRepository : IAssuntoRepository
    {
        protected readonly ISession Session;

        public AssuntoRepository(ISession session)
        {
            Session = session;
        }

        public IList<Assunto> Find(string part)
        {
            return Session.QueryOver<Assunto>().Where(x => x.DsAssunto.IsLike(part, MatchMode.Anywhere)).OrderBy(x => x.DsAssunto).Asc.List();
        }
    }

    public interface IAssuntoRepository : IRepository
    {
        IList<Assunto> Find(string part);
    }
}