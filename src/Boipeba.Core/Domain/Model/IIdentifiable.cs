namespace Boipeba.Core.Domain.Model
{
    /// <summary>
    /// Abstração de uma entidade persistente da aplicação identificável por Id.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Identificador PK
        /// </summary>
        long Id { get; set; }
    }
}
