namespace Boipeba.Core.Domain.Repositories
{
    /// <summary>
    /// Interface de CRUD genérico.
    /// </summary>
    public interface ICrudRepository<T, TId>: IRepository
    {
        /// <summary>
        /// Persistir entidade transiente.
        /// </summary>
        T Add(T model);

        /// <summary>
        /// Atualizar entidade persistente.
        /// </summary>
        T Update(T model);

        /// <summary>
        /// Adicionar entidade transiente ou atualizar entidade persistente.
        /// </summary>
        T AddOrUpdate(T model);

        /// <summary>
        /// Remover entidade persistente.
        /// </summary>
        void Remove(T model);

        /// <summary>
        /// Remover entidade persistente.
        /// </summary>
        void Remove(TId id);

        /// <summary>
        /// Encontrar entidade persistente.
        /// </summary>
        T Find(TId id);
    }
}