using Boipeba.Core.Domain.Model;
using NHibernate;

namespace Boipeba.Core.Domain.Repositories
{
    /// <summary>
    /// Componente de CRUD genérico.
    /// </summary>
    public class DefaultRepository<T, ID>: ICrudRepository<T, ID>
        where T: IIdentifiable
    {
#pragma warning disable 1591
        protected ISession Session;
        
        public DefaultRepository(ISession session)

        {
            Session = session;
        }
#pragma warning restore 1591

        /// <summary>
        /// Persistir entidade transiente.
        /// </summary>
        public T Add(T model)
        {
            Session.Save(model);

            return model;
        }

        /// <summary>
        /// Atualizar entidade persistente.
        /// </summary>
        public T Update(T model)
        {
            Session.Update(model);

            return model;
        }

        /// <summary>
        /// Adicionar entidade transiente ou atualizar entidade persistente.
        /// </summary>
        public T AddOrUpdate(T model)
        {
            return model.Id == 0 ? Add(model) : Update(model);
        }

        /// <summary>
        /// Remover entidade persistente.
        /// </summary>
        public void Remove(T model)
        {
            Session.Delete(model);
        }

        /// <summary>
        /// Remover entidade persistente.
        /// </summary>
        public void Remove(ID id)
        {
            Remove(Find(id));
        }

        /// <summary>
        /// Encontrar entidade persistente.
        /// </summary>
        public T Find(ID id)
        {
            return Session.Get<T>(id);
        }
    }
}