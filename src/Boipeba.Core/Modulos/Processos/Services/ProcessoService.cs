using System;
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

            processo.Autor = autor;

            if (processo.Destinatario.Tipo.Equals(new OrgaoUnidade().GetType().Name))
            {
                processo.OrgaoUnidadeDestino = new OrgaoUnidade
                {
                    IdOrgaoUnidade = processo.Destinatario.Id,
                    DsOrgaoUnidade = processo.Destinatario.Descricao
                };
            }
            else if (processo.Destinatario.Tipo.Equals(new Pessoa().GetType().Name))
            {
                processo.PessoaDestino = new Pessoa
                {
                    Matricula = processo.Destinatario.Id,
                    Nome = processo.Destinatario.Descricao
                };
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

            processo.UltimoMovimento = processoMovimentoEncaminhamento;

            _processoRepository.AddOrUpdate(processo);

            return processo;
        }

        public ProcessoMovimento Movimentar(ProcessoMovimento processoMovimento)
        {
            var agora = DateTime.Now;

            var processo = _processoRepository.Find(processoMovimento.Processo.Id);

            processoMovimento.Processo = processo;

            if (MovimentoRepetido(processoMovimento))
                throw new MovimentoRepetidoException();

            processoMovimento.Data = agora;

            processo.UltimoMovimento = _processoMovimentoRepository.Add(processoMovimento);

            if (ProcessoEncaminhavel(processoMovimento))
            {
                processo.UltimoMovimento = Encaminhar(processoMovimento);
                processo.PessoaDestino = processo.UltimoMovimento.PessoaDestino;
                processo.OrgaoUnidadeDestino = processo.UltimoMovimento.OrgaoUnidadeDestino;
            }

            processo.UltimaModificacao = agora;

            _processoRepository.AddOrUpdate(processo);

            return processoMovimento;
        }

        public ProcessoMovimento Encaminhar(ProcessoMovimento processoMovimento)
        {
            var movimentoEncaminhamento = new ProcessoMovimento
            {
                Processo = processoMovimento.Processo,
                Data = DateTime.Now,
                Movimento = new Movimento
                {
                    CdMovimento = _processoSettings.CodigoMovimentoEncaminhamentoOrgaoInterno
                }
            };

            if (processoMovimento.Destino.Tipo.Equals(new OrgaoUnidade().GetType().Name))
            {
                movimentoEncaminhamento.OrgaoUnidadeDestino = new OrgaoUnidade
                {
                    IdOrgaoUnidade = processoMovimento.Destino.Id,
                    DsOrgaoUnidade = processoMovimento.Destino.Descricao
                };
            }
            else if (processoMovimento.Destino.Tipo.Equals(new Pessoa().GetType().Name))
            {
                movimentoEncaminhamento.PessoaDestino = new Pessoa
                {
                    Matricula = processoMovimento.Destino.Id,
                    Nome = processoMovimento.Destino.Descricao
                };
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
            var processo = _processoRepository.Find(processoMovimento.Processo.Id);
            return processo.UltimoMovimento.Movimento.CdMovimento.Equals(processoMovimento.Movimento
                .CdMovimento);
        }
    }
}