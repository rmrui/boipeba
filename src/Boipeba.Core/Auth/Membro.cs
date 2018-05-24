#pragma warning disable 1591

using System;
using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Auth
{
    /// <summary>
    /// View Membro
    /// </summary>
    public class Membro: IIdentifiable
    {
        /// <summary>
        /// Matricula
        /// </summary>
        public virtual long Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Email { get; set; }
    }
}
