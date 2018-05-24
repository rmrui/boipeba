#pragma warning disable 1591
using System;

namespace Boipeba.Core.Domain.Value
{
    /// <summary>
    /// Value Object de abstração de um periodo temporal.
    /// </summary>
    public class Periodo
    {
        public Periodo()
        {

        }

        /// <summary>
        /// Cria instância do período com a hora 23:59:59 na data final.
        /// </summary>
        public Periodo(DateTime inicio, DateTime fim)
        {
            Inicio = inicio;

            var shouldFix = fim.Hour == 0 && fim.Minute == 0 & fim.Second == 0;

            Fim = shouldFix ? fim.AddDays(1).AddSeconds(-1) : fim;
        }

        /// <summary>
        /// Cria instância do período apenas ano
        /// </summary>
        public Periodo(int inicio, int fim)
        {
            Inicio = new DateTime(inicio, 1, 1);
            Fim = new DateTime(fim, 12, 31);
            Ano = true;
        }

        /// <summary>
        /// Cria instância do período para a quantidade de dias passados a partir de hoje.
        /// </summary>
        public static Periodo Last(int days)
        {
            return new Periodo
            {
                Fim = DateTime.Now,
                Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-days)
            };
        }

        /// <summary>
        /// Cria instância do período para o periodo do dia 00:00 até o horário do momento
        /// </summary>
        public static Periodo Hoje()
        {
            return new Periodo
            {
                Fim = DateTime.Now,
                Inicio = DateTime.Today
            };
        }

        /// <summary>
        /// Ajusta data Fim para último segundo do dia 23:59:59
        /// </summary>
        public Periodo AjustarHorarioFinal()
        {
            Fim = Fim.AddDays(1).AddSeconds(-1);

            return this;
        }

        public DateTime Inicio { get; set; }

        public DateTime Fim { get; set; }

        public bool Ano { get; set; }

        private int _anoInicio = 0;
        private int _anoFim = 0;

        public int AnoInicio
        {
            get { return _anoInicio; }
            set
            {
                _anoInicio = value;
                Ano = true;
            }
        }

        public int AnoFim
        {
            get { return _anoFim; }
            set
            {
                _anoFim = value;
                Ano = true;
            }
        }

        public override string ToString()
        {
            return Ano? $"{AnoInicio} à {AnoFim}": $"{Inicio} à {Fim}";
        }
    }
}