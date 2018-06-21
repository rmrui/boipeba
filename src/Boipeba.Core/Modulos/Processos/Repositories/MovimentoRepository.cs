using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public interface IMovimentoRepository : IRepository
    {
        IList<Movimento> Find(string part);
        Movimento Get(long id);
    }

    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly ISession Session;

        public MovimentoRepository(ISession session)
        {
            Session = session;
        }

        public IList<Movimento> Find(string part)
        {
            return Session.QueryOver<Movimento>().Where(x => x.DsMovimento.IsLike(part, MatchMode.Anywhere))
                .OrderBy(x => x.DsMovimento).Asc.List();
        }

        public Movimento Get(long id)
        {
            return Session.Get<Movimento>(id);
        }
    }
}