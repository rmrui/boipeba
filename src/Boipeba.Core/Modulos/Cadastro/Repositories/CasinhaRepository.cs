using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;

namespace Boipeba.Core.Modulos.Cadastro.Repositories
{
    public interface ICasinhaRepository : ICrudRepository<Casinha, long>
    {
        IList<Casinha> FindAll();
    }

    public class CasinhaRepository : DefaultRepository<Casinha, long>, ICasinhaRepository
    {
        public CasinhaRepository(ISession session) : base(session)
        {
        }

        public IList<Casinha> FindAll()
        {
            return Session.QueryOver<Casinha>().List();
        }
    }


}
