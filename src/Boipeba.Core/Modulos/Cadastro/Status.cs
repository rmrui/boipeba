using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Cadastro
{
    public class Status: IIdentifiable
    {
        public virtual long Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Descricao { get; set; }

        public virtual bool Fechamento { get; set; }
    }
}