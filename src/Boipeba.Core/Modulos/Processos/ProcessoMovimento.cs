using System;
using System.Web.Script.Serialization;
using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Processos
{
    public class ProcessoMovimento: IIdentifiable
    {
        public virtual long Id { get; set; }

        [ScriptIgnore]
        public virtual Processo Processo { get; set; }

        public virtual DateTime Data { get; set; }

        public virtual Pessoa Autor { get; set; }

        public virtual Movimento Movimento { get; set; }

        public virtual string Complemento { get; set; }

        public virtual OrgaoUnidade OrgaoUnidadeOrigem { get; set; }

        public virtual Pessoa PessoaOrigem { get; set; }

        public virtual OrgaoUnidade OrgaoUnidadeDestino { get; set; }

        public virtual Pessoa PessoaDestino { get; set; }

        public virtual IdentifiableDescriptionItem Destino { get; set; }

        //public Arquivo[] Arquivos { get; set; }

        //public bool Start => Origem == null;
        //public bool End { get; set; }
    }

    public class Arquivo
    {
        public string Nome { get; set; }

        public string Hash { get; set; }

        public string Path { get; set; }

        public ProcessoMovimento ProcessoMovimento { get; set; }
    }
}