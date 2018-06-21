using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Processos.Map
{
    public class PessoaMap : ClassMapping<Pessoa>
    {
        public PessoaMap()
        {
            Id(x => x.Matricula, y =>
            {
                y.Generator(Generators.Assigned);
                y.Column("NuMatricula");
            });

            Property(x => x.Nome, y => y.Column("NmPessoa"));

            ManyToOne(x => x.OrgaoUnidadeLotacao, y => y.Column("IdOuLotacao"));
        }
    }
}