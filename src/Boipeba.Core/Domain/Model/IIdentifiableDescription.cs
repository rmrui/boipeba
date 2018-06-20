namespace Boipeba.Core.Domain.Model
{
    public interface IIdentifiableDescription: IIdentifiable
    {
        string Descricao { get; set; }
    }
}