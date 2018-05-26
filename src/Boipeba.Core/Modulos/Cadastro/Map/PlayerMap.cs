using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Cadastro.Map
{
    public class PlayerMap : ClassMapping<Player>
    {
        public PlayerMap()
        {
            Table("tPlayer");

            Id(x => x.Id, c =>
            {
                c.Generator(Generators.Identity);
                c.Column("IdPlayer");
            });

            Property(x => x.Name);

            Property(x => x.Position);

            Property(x => x.Team);
        }
    }
}