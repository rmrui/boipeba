using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public class OrgaoUnidadeRepository : IOrgaoUnidadeRepository
    {
        protected readonly ISession Session;

        public OrgaoUnidadeRepository(ISession session)
        {
            Session = session;
        }

        public IList<OrgaoUnidade> Find(string part)
        {
            return Session.QueryOver<OrgaoUnidade>()
                .Where(x => x.DsOrgaoUnidade.IsInsensitiveLike(part, MatchMode.Anywhere)).OrderBy(x => x.DsOrgaoUnidade).Asc.List();
        }

        public IEnumerable<OrgaoUnidade> FindAll()
        {
            return Session.QueryOver<OrgaoUnidade>().List();
        }

        public OrgaoUnidade Find(int id)
        {
            return Session.Get<OrgaoUnidade>(id);
        }
    }

    public interface IOrgaoUnidadeRepository : IRepository
    {
        IList<OrgaoUnidade> Find(string part);
        IEnumerable<OrgaoUnidade> FindAll();
        OrgaoUnidade Find(int id);
    }
}