using System;
using Boipeba.Core.Auth;
using Boipeba.Core.Auth.Services;
using Boipeba.Core.Domain.Model;
using Boipeba.Core.Domain.Services;
using Boipeba.Core.Modulos.Processos.Exceptions;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Core.Modulos.Processos.Services
{
    public interface IProcessoService : IService
    {
        Processo Salvar(Processo processo);
        ProcessoMovimento Movimentar(ProcessoMovimento processoMovimento);
    }

    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IProcessoMovimentoRepository _processoMovimentoRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly ProcessoSettings _processoSettings;

        public ProcessoService(IProcessoMovimentoRepository processoMovimentoRepository,
            IProcessoRepository processoRepository,
            IPessoaRepository pessoaRepository,
            IMovimentoRepository movimentoRepository, ProcessoSettings processoSettings)
        {
            _processoMovimentoRepository = processoMovimentoRepository;
            _processoRepository = processoRepository;
            _pessoaRepository = pessoaRepository;
            _movimentoRepository = movimentoRepository;
            _processoSettings = processoSettings;
        }

        public Processo Salvar(Processo processo)
        {
            var agora = DateTime.Now;

            processo.UltimaModificacao = agora;

            var autor = _pessoaRepository.Find(processo.Autor.Id);

            if (processo.Destinatario.Tipo.Equals(new OrgaoUnidade().GetType().Name))
            {
                processo.OrgaoUnidadeDestino = ((OrgaoUnidade)processo.Destinatario).FromIdentifiableDescription();
            }
            else if (processo.Destinatario.Tipo.Equals(new Pessoa().GetType().Name))
            {
                processo.PessoaDestino = ((Pessoa)processo.Destinatario).FromIdentifiableDescription();
            }
            else
            {
                throw new NotImplementedException("Destinatário inválido.");
            }

            _processoRepository.AddOrUpdate(processo);

            var processoMovimentoEncaminhamento = new ProcessoMovimento
            {
                Data = agora,
                PessoaOrigem = autor,
                OrgaoUnidadeOrigem = autor.OrgaoUnidadeLotacao,
                PessoaDestino = processo.PessoaDestino,
                OrgaoUnidadeDestino = processo.OrgaoUnidadeDestino,
                Autor = autor,
                Processo = processo,
                Movimento = _movimentoRepository.Get(_processoSettings.CodigoMovimentoEncaminhamentoOrgaoInterno)
            };

            _processoMovimentoRepository.Add(processoMovimentoEncaminhamento);

            return processo;
        }

        public ProcessoMovimento Movimentar(ProcessoMovimento processoMovimento)
        {
            processoMovimento.Processo = _processoRepository.Find(processoMovimento.Processo.Id);

            if (MovimentoRepetido(processoMovimento))
                throw new MovimentoRepetidoException();

            processoMovimento.Data = DateTime.Now;

            _processoMovimentoRepository.Add(processoMovimento);

            if (ProcessoEncaminhavel(processoMovimento))
            {
                Encaminhar(processoMovimento);
            }

            return processoMovimento;
        }

        public ProcessoMovimento Encaminhar(ProcessoMovimento processoMovimento)
        {
            var movimentoEncaminhamento = new ProcessoMovimento
            {
                Processo = processoMovimento.Processo,
                Data = DateTime.Now,
                Movimento =
                {
                    CdMovimento = _processoSettings.CodigoMovimentoEncaminhamentoOrgaoInterno
                }
            };


            if (processoMovimento.Destino.Tipo.Equals(new OrgaoUnidade().GetType().Name))
            {
                movimentoEncaminhamento.OrgaoUnidadeDestino = ((OrgaoUnidade)processoMovimento.Destino).FromIdentifiableDescription();
            }
            else if (processoMovimento.Destino.Tipo.Equals(new Pessoa().GetType().Name))
            {
                movimentoEncaminhamento.PessoaDestino = ((Pessoa)processoMovimento.Destino).FromIdentifiableDescription();
            }
            else
            {
                throw new NotImplementedException("Destinatário inválido.");
            }

            _processoMovimentoRepository.Add(movimentoEncaminhamento);
            return movimentoEncaminhamento;
        }

        private bool ProcessoEncaminhavel(ProcessoMovimento processoMovimento)
        {
            return processoMovimento.Destino != null;
        }

        private bool MovimentoRepetido(ProcessoMovimento processoMovimento)
        {
            return processoMovimento.Processo.UltimoMovimento.Movimento.CdMovimento.Equals(processoMovimento.Movimento
                .CdMovimento);
        }
    }
}