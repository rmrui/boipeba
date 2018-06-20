using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Processos
{

    public class Pessoa
    {
        public virtual long Matricula { get; set; }

        public virtual string Nome { get; set; }

        public virtual bool IsMembro { get; set; }
    }
}