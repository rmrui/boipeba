namespace Boipeba.Core.Modulos.Processos
{
    public class Assignee
    {
        public Pessoa Pessoa { get; set; }

        public OrgaoUnidade OrgaoUnidade { get; set; }

        public long? Id => Pessoa?.Matricula ?? OrgaoUnidade?.IdOrgaoUnidade;

        public string Descricao => Pessoa?.Nome ?? OrgaoUnidade?.DsOrgaoUnidade;
    }
}