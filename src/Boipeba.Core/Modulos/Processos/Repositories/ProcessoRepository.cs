using Boipeba.Core.Domain.Repositories;
using NHibernate;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public class ProcessoRepository : DefaultRepository<Processo, long>, IProcessoRepository
    {
        public ProcessoRepository(ISession session) : base(session)
        {
        }
    }

    public interface IProcessoRepository : ICrudRepository<Processo, long>
    {

    }
}