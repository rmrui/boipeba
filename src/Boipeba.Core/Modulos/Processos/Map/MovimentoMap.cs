﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Processos.Map
{
    public class MovimentoMap : ClassMapping<Movimento>
    {
        public MovimentoMap()
        {
            Id(x => x.CdMovimento, y => y.Generator(Generators.Assigned));
            Property(x => x.DsMovimento);
        }
    }
}