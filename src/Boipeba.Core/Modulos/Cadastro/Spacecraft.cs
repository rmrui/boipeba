using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Cadastro
{
    public class Spacecraft: IIdentifiable
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Agency { get; set; }
    }
}