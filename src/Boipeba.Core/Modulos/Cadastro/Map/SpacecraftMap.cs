﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Cadastro.Map
{
    public class SpacecraftMap : ClassMapping<Spacecraft>
    {
        public SpacecraftMap()
        {
#if DEBUG
            Table("tSpacecraft");
#endif

            Id(x => x.Id, c =>
            {
                c.Generator(Generators.Identity);
                c.Column("IdSpacecraft");
            });

            Property(x => x.Name);

            Property(x => x.Agency);
        }
    }
}