using System;
using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Cadastro
{
    public class Casinha : IIdentifiable
    {
        public virtual long Id { get; set; }

        public virtual string Nome { get; set; }
        
        public virtual string Rua { get; set; }

        public virtual string Numero { get; set; }

        public virtual DateTime? Data { get; set; }
    }
}
