using Boipeba.Core.Domain.Repositories;
using NHibernate;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public interface IProcessoMovimentoRepository : ICrudRepository<ProcessoMovimento, long>
    {
        
    }

    public class ProcessoMovimentoRepository: DefaultRepository<ProcessoMovimento, long>, IProcessoMovimentoRepository
    {
        public ProcessoMovimentoRepository(ISession session) : base(session)
        {

        }
    }
}