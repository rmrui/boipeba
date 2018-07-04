using System.Collections.Generic;
using Boipeba.Core.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;

namespace Boipeba.Core.Modulos.Processos.Repositories
{
    public interface IProcessoRepository : ICrudRepository<Processo, long>
    {
        IList<Processo> AtribuidosPara(int matricula);
        IList<ProcessoMovimento> HistoricoProcesso(long idProcesso);
    }

    public class ProcessoRepository : DefaultRepository<Processo, long>, IProcessoRepository
    {
        private readonly IPessoaRepository _pessoaRepository;

        public ProcessoRepository(ISession session, IPessoaRepository pessoaRepository) : base(session)
        {
            _pessoaRepository = pessoaRepository;
        }

        public IList<Processo> AtribuidosPara(int matricula)
        {
            var pessoa = _pessoaRepository.Find(matricula);
            Processo processo = null;
            Pessoa pessoaDestino = null;
            OrgaoUnidade orgaoUnidadeDestino = null;

            var consulta = Session.QueryOver<Processo>(() => processo);

            consulta.Left.JoinQueryOver(() => processo.PessoaDestino, () => pessoaDestino);

            consulta.Left.JoinQueryOver(() => processo.OrgaoUnidadeDestino, () => orgaoUnidadeDestino);

            var condicoes = Restrictions.Disjunction();

            condicoes.Add(Restrictions.Eq(
                Projections.Property(() => orgaoUnidadeDestino.IdOrgaoUnidade),
                pessoa.OrgaoUnidadeLotacao.IdOrgaoUnidade));

            condicoes.Add(Restrictions.Eq(Projections.Property(() => pessoaDestino.Matricula),
                pessoa.Matricula));


            return consulta.Where(condicoes).OrderBy(() => processo.UltimaModificacao).Desc.List();
        }

        public IList<ProcessoMovimento> HistoricoProcesso(long idProcesso)
        {
            return Session.QueryOver<ProcessoMovimento>().Where(x => x.Processo.Id == idProcesso).OrderBy(x => x.Data)
                .Desc.List();
        }
    }
}