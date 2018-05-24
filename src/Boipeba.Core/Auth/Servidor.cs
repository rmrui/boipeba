#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Auth
{
    public class Servidor : IIdentifiable
    {
        /// <summary>
        /// Matricula
        /// </summary>
        public virtual long Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Email { get; set; }

        public virtual string Login { get; set; }
    }
}
