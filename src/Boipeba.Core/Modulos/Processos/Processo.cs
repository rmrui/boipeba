using System;
using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Processos
{
    public class Processo : IIdentifiable
    {
        public virtual long Id { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual string Simp { get; set; }
        public virtual bool Urgente { get; set; }
        public virtual bool Reservado { get; set; }
        public virtual Pessoa Interessado { get; set; }
        public virtual OrgaoUnidade OrgaoUnidadeInteressado { get; set; }
        public virtual bool Sociedade { get; set; }
        public virtual Assunto Assunto { get; set; }
        public virtual string Complemento { get; set; }
    }
}