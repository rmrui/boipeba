using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Processos
{

    public class Pessoa : IdentifiableDescriptionItem
    {
        public virtual long Matricula { get; set; }

        public virtual string Nome { get; set; }

        public virtual bool IsMembro { get; set; }

        public virtual OrgaoUnidade OrgaoUnidadeLotacao { get; set; }

        public virtual bool Ativo { get; set; }

        public static Pessoa FromIdentifiableDescriptionItem(IdentifiableDescriptionItem item)
        {
            return new Pessoa
            {
                Matricula = item.Id,
                Nome = item.Descricao
            };
        }
    }
}