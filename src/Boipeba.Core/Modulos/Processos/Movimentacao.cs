using System;
using Boipeba.Core.Domain.Model;

namespace Boipeba.Core.Modulos.Processos
{
    public class Movimentacao: IIdentifiable
    {
        public long Id { get; set; }

        public Processo Processo { get; set; }

        public DateTime Data { get; set; }

        public Pessoa Autor { get; set; }

        public string Parecer { get; set; }

        public Assignee Origem { get; set; }

        public Assignee Destino { get; set; }
        
        //public Arquivo[] Arquivos { get; set; }

        //public bool Start => Origem == null;
        //public bool End { get; set; }
    }

    public class Arquivo
    {
        public string Nome { get; set; }

        public string Hash { get; set; }

        public string Path { get; set; }

        public Movimentacao Movimentacao { get; set; }
    }
}