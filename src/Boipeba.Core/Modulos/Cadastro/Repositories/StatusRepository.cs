using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;

namespace Boipeba.Core.Modulos.Cadastro.Repositories
{
    public interface IStatusRepository : ICrudRepository<Status, long>
    {
        IList<Status> FindAll();
    }

    public class StatusRepository : DefaultRepository<Status, long>, IStatusRepository
    {
        public StatusRepository(ISession session) : base(session)
        {
        }

        public IList<Status> FindAll()
        {
            return Session.QueryOver<Status>().List();
        }
    }
}