using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Cadastro
{
    public class Player: IIdentifiable
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Position { get; set; }

        public virtual string Team { get; set; }
    }
}