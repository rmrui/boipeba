using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Cadastro.Map
{
    public class StatusMap: ClassMapping<Status>
    {
        public StatusMap()
        {
#if DEBUG
            Table("tStatus");
#endif

            Id(x => x.Id, c =>
            {
                c.Generator(Generators.Identity);
                c.Column("IdStatus");
            });

            Property(x => x.Nome, c => c.Column("NmStatus"));

            Property(x => x.Descricao, c => c.Column("DsStatus"));

            Property(x => x.Fechamento, c => c.Column("StFechamento"));
        }
    }
}