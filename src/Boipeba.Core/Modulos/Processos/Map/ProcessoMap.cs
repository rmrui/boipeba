using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Processos.Map
{
    public class ProcessoMap : ClassMapping<Processo>
    {
        public ProcessoMap()
        {
            Id(x => x.Id, y => { y.Column("IdProcesso"); y.Generator(Generators.Identity); });
            Property(x => x.Cadastro, y => y.Column("DtCadastro"));
            Property(x => x.Simp, y => y.Column("NuSimp"));
            Property(x => x.Urgente, y => y.Column("StUrgente"));
            Property(x => x.Reservado, y => y.Column("StReservado"));
            Property(x => x.Sociedade, y => y.Column("StSociedadeInteressada"));
            Property(x => x.Complemento, y => y.Column("DsComplementoAssunto"));

            ManyToOne<Pessoa>(x => x.Interessado, y => y.Column("NuMatriculaInteressado"));
            ManyToOne<OrgaoUnidade>(x => x.OrgaoUnidadeInteressado, y => y.Column("IdOuInteressado"));
            ManyToOne<Assunto>(x => x.Assunto, y => y.Column("CdAssunto"));
        }
    }
    public class MovimentacaoMap : ClassMapping<Movimentacao>
    {
        public MovimentacaoMap()
        {
            Id(x => x.Id, y => { y.Column("IdMovimentacao"); y.Generator(Generators.Identity); });

            Property(x => x.Data, y => y.Column("DtMovimentacao"));

            ManyToOne<Pessoa>(x => x.Autor, y => y.Column("NuMatriculaResponsavel"));

            Property(x => x.Parecer, y => y.Column("DsParecer"));

            Component(x => x.Origem, c =>
            {
                c.Property("Pessoa", m => m.Column("NuMatricula"));
                c.Property("OrgaoUnidade", m => m.Column("IdOrgaoUnidade"));
            });

            //Component(x => x.Destino, c =>
            //{
            //    c.Property(y => y.Pessoa, m => m.Column("NuMatriculaDestino"));
            //    c.Property(x => x.OrgaoUnidade, m => m.Column("IdOrgaoUnidadeDestino"));
            //});
        }
    }
}