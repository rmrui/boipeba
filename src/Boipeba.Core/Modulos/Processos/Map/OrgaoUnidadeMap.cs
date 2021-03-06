﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Processos.Map
{
    public class OrgaoUnidadeMap : ClassMapping<OrgaoUnidade>
    {
        public OrgaoUnidadeMap()
        {
            Table("vw_OrgaoUnidade");
            Id(x => x.IdOrgaoUnidade, y => y.Generator(Generators.Assigned));
            Property(x => x.DsOrgaoUnidade);
            Property(x => x.Atributos);
        }
    }
}