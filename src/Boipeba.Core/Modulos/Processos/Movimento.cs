namespace Boipeba.Core.Modulos.Processos
{
    public class Movimento
    {
        public virtual long CdMovimento { get; set; }
        public virtual string DsMovimento { get; set; }
        public virtual string DsMovimentoSimples { get; set; }
        public virtual string DsMovimentoTitulo => DsMovimentoSimples.Replace("/", " / ");
        public virtual string DsGlossario { get; set; }
    }
}