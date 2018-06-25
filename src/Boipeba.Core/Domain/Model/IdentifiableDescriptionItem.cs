namespace Boipeba.Core.Domain.Model
{
    public class IdentifiableDescriptionItem: IIdentifiableDescription
    {
        public virtual long Id { get; set; }

        public virtual string Descricao { get; set; }

        public virtual string Tipo { get; set; }
    }
}