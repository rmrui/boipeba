#pragma warning disable 1591
using System;
using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Auth
{
    public class Acesso : IIdentifiable
    {
        public virtual long Id { get; set; }

        public virtual Servidor Servidor { get; set; }

        public virtual Membro Membro { get; set; }

        public virtual DateTime Data { get; set; }

        public virtual string IP { get; set; }

        public virtual bool RememberMe { get; set; }

        public virtual bool ReturnSigned { get; set; }

        public virtual bool MobileBrowser { get; set; }
        
        public virtual string FormaAcesso => MobileBrowser ? "Mobile" : "Desktop";
    }
}
