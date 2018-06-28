using System;
using Boipeba.Core.Auth;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Domain.Services;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Core.Modulos.Processos.Services
{
    public interface IProcessoService : IService
    {
        Processo Cadastrar(Processo processo);

        ProcessoMovimento Movimentar(Processo processo, Pessoa autor, Pessoa pessoaDestinatario, OrgaoUnidade orgaoUnidadeDestinatario, string parecer);
    }

    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IProcessoMovimentoRepository _processoMovimentoRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly GlobalSettings _globalSettings;

        public ProcessoService(IProcessoMovimentoRepository processoMovimentoRepository, 
            IProcessoRepository processoRepository, 
            IPessoaRepository pessoaRepository, 
            IMovimentoRepository movimentoRepository, 
            GlobalSettings globalSettings)
        {
            _processoMovimentoRepository = processoMovimentoRepository;
            _processoRepository = processoRepository;
            _pessoaRepository = pessoaRepository;
            _movimentoRepository = movimentoRepository;
            _globalSettings = globalSettings;
        }

        public Processo Cadastrar(Processo processo)
        {
            var agora = DateTime.Now;

            processo.UltimaModificacao = agora;

            var autor = _pessoaRepository.Find(processo.Autor.Id);

            //processo.Autor = new Pessoa{ Id = matriculaAutor };

            if (processo.Destinatario.Tipo.Equals(new OrgaoUnidade().GetType().Name))
            {
                processo.OrgaoUnidadeDestino = OrgaoUnidade.FromIdenfiableDescriptionItem(processo.Destinatario);
            }
            else if (processo.Destinatario.Tipo.Equals(new Pessoa().GetType().Name))
            {
                processo.PessoaDestino = Pessoa.FromIdentifiableDescriptionItem(processo.Destinatario);
            }
            else
            {
                throw new NotImplementedException("Destinatário inválido.");
            }

            _processoRepository.Add(processo);

            var processoMovimentoEncaminhamento = new ProcessoMovimento
            {
                Data = agora,
                PessoaOrigem = autor,
                OrgaoUnidadeOrigem = autor.OrgaoUnidadeLotacao,
                PessoaDestino = processo.PessoaDestino,
                OrgaoUnidadeDestino = processo.OrgaoUnidadeDestino,
                Autor = autor,
                Processo = processo,
                Movimento = _movimentoRepository.Get(_globalSettings.MovimentoEncaminhamentoOrgaoInterno)
            };

            _processoMovimentoRepository.Add(processoMovimentoEncaminhamento);

            return processo;
        }

        public ProcessoMovimento Movimentar(Processo processo, Pessoa autor, Pessoa pessoaDestinatario, OrgaoUnidade orgaoUnidadeDestinatario, string parecer)
        {
            var movimento = new ProcessoMovimento
            {
                Data = DateTime.Now,
                PessoaOrigem = autor,
                PessoaDestino = pessoaDestinatario,
                OrgaoUnidadeDestino = orgaoUnidadeDestinatario,
                Autor = autor,
                Processo = processo,
                Complemento = parecer
            };

            _processoMovimentoRepository.Add(movimento);

            return movimento;
        }
    }
}