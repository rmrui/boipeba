using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Boipeba.Core.Domain.Model;
using Boipeba.Core.Modulos.Workflow;
using NHibernate.Tuple;

namespace Boipeba.Core.Modulos.Processos
{
    public class Processo : IFlowable
    {
        public virtual long Id { get; set; }

        public virtual DateTime Cadastro { get; set; }

        public virtual string Simp { get; set; }

        public virtual bool Urgente { get; set; }

        public virtual bool Reservado { get; set; }

        public virtual string Interessado { get; set; }

        public virtual OrgaoUnidade OrgaoUnidadeInteressado { get; set; }

        public virtual bool Sociedade { get; set; }

        public virtual Assunto Assunto { get; set; }

        public virtual string Complemento { get; set; }

        public virtual Pessoa Autor { get; set; }

        public virtual Pessoa PessoaDestino { get; set; }

        public virtual OrgaoUnidade OrgaoUnidadeDestino { get; set; }

        public virtual IdentifiableDescriptionItem Destinatario { get; set; }

        public virtual DateTime UltimaModificacao { get; set; }

        public virtual string Destino => OrgaoUnidadeDestino?.DsOrgaoUnidade ?? PessoaDestino?.Nome;

        public virtual ProcessoMovimento UltimoMovimento { get; set; }
    }
}