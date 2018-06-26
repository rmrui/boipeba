using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Boipeba.Core.Modulos.Processos.Map
{
    public class ProcessoMap : ClassMapping<Processo>
    {
        public ProcessoMap()
        {
            Table("tProcesso");
            Id(x => x.Id, y => { y.Column("IdProcesso"); y.Generator(Generators.Identity); });
            Property(x => x.Cadastro, y => y.Column("DtCadastro"));
            Property(x => x.Simp, y => y.Column("NuSimp"));
            Property(x => x.Urgente, y => y.Column("StUrgente"));
            Property(x => x.Reservado, y => y.Column("StReservado"));
            Property(x => x.Sociedade, y => y.Column("StSociedadeInteressada"));
            Property(x => x.Complemento, y => y.Column("DsComplementoAssunto"));
            Property(x => x.Interessado, y => y.Column("NmInteressado"));
            Property(x => x.UltimaModificacao, y => y.Column("DtUltimaModificacao"));
            ManyToOne<OrgaoUnidade>(x => x.OrgaoUnidadeInteressado, y => y.Column("IdOuInteressado"));
            ManyToOne<Assunto>(x => x.Assunto, y => y.Column("CdAssunto"));
            ManyToOne<Pessoa>(x => x.Autor, y => y.Column("NuMatriculaAutor"));
            ManyToOne(x => x.OrgaoUnidadeDestino, y => y.Column("IdOuDestino"));
            ManyToOne(x => x.PessoaDestino, y => y.Column("NuMatriculaDestino"));
        }
    }

    public class ProcessoMovimentoMap : ClassMapping<ProcessoMovimento>
    {
        public ProcessoMovimentoMap()
        {
            Table("tProcessoMovimento");

            Id(x => x.Id, y => { y.Column("IdProcessoMovimento"); y.Generator(Generators.Identity); });

            Property(x => x.Data, y => y.Column("DtMovimentacao"));

            Property(x => x.Complemento, y => y.Column("DsComplemento"));

            ManyToOne(x => x.Processo, y => y.Column("IdProcesso"));

            ManyToOne(x => x.Autor, y => y.Column("NuMatriculaAutor"));

            ManyToOne(x => x.Movimento, y => y.Column("CdMovimento"));

            ManyToOne(x => x.OrgaoUnidadeOrigem, y => { y.Column("IdOrgaoUnidadeOrigem"); });

            ManyToOne(x => x.PessoaOrigem, y => { y.Column("NuMatriculaOrigem"); });

            ManyToOne(x => x.OrgaoUnidadeDestino, y => { y.Column("IdOrgaoUnidadeDestino"); });

            ManyToOne(x => x.PessoaDestino, y => { y.Column("NuMatriculaDestino"); });
        }
    }
}