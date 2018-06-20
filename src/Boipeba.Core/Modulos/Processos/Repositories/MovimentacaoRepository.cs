using Boipeba.Core.Domain.Repositories;
using NHibernate;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public interface IMovimentacaoRepository : ICrudRepository<Movimentacao, long>
    {
        
    }

    public class MovimentacaoRepository: DefaultRepository<Movimentacao, long>, IMovimentacaoRepository
    {
        public MovimentacaoRepository(ISession session) : base(session)
        {

        }
    }
}