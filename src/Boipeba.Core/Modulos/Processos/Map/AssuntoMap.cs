﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Processos.Map
{
    public class AssuntoMap : ClassMapping<Assunto>
    {
        public AssuntoMap()
        {
            Table("vw_Assunto");
            Id(x => x.CdAssunto, y => y.Generator(Generators.Assigned));
            Property(x => x.DsAssunto);
        }
    }
}