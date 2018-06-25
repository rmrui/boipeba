using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Processos
{
    public class OrgaoUnidade : IdentifiableDescriptionItem
    {
        public virtual long IdOrgaoUnidade { get; set; }

        public virtual string DsOrgaoUnidade { get; set; }

        public static OrgaoUnidade FromIdenfiableDescriptionItem(IdentifiableDescriptionItem item)
        {
            return new OrgaoUnidade
            {
                IdOrgaoUnidade = item.Id,
                DsOrgaoUnidade = item.Descricao
            };
        }
    }
}