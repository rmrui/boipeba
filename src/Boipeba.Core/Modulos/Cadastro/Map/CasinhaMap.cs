using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Cadastro.Map
{
    public class CasinhaMap : ClassMapping<Casinha>
    {
        public CasinhaMap()
        {
            Table("tCasinha");

            Id(x => x.Id, y =>
            {
                y.Column("IdCasinha");

                y.Generator(Generators.Identity);
            });

            Property(x => x.Nome, y => y.Column("NmCasinha"));
            Property(x => x.Rua, y => y.Column("DsRua"));
            Property(x => x.Numero, y => y.Column("NuCasinha"));
            Property(x => x.Data, y => y.Column("DtCasinha"));
        }
    }
}
