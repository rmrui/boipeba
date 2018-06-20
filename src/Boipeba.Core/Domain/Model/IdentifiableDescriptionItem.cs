namespace Boipeba.Core.Domain.Model
{
    public class IdentifiableDescriptionItem: IIdentifiableDescription
    {
        public long Id { get; set; }

        public string Descricao { get; set; }
    }
}