﻿using System.Web.Script.Serialization;
using Boipeba.Core.Domain.Model;
using NHibernate.Criterion;

namespace Boipeba.Core.Modulos.Processos
{
    public class OrgaoUnidade : IdentifiableDescriptionItem
    {
        public virtual long IdOrgaoUnidade { get; set; }

        public virtual string DsOrgaoUnidade { get; set; }

        [ScriptIgnore]
        public virtual string Atributos { get; set; }

        public virtual bool IsTramitacaoDocumentos => Atributos.Contains("D");

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