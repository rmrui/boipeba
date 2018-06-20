using System;
using Boipeba.Core.Modulos.Processos.Repositories;

namespace Boipeba.Core.Modulos.Processos.Services
{
    public interface IProcessoService
    {
        Processo Cadastrar(Processo processo);
        Movimentacao Movimentar(Processo processo, Pessoa autor, Assignee destinatario, string parecer);
    }

    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public ProcessoService(IMovimentacaoRepository movimentacaoRepository, IProcessoRepository processoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
            _processoRepository = processoRepository;
        }

        public Processo Cadastrar(Processo processo)
        {
            _processoRepository.Add(processo);

            var movimento = new Movimentacao
            {
                Data = DateTime.Now,
                Destino = processo.Destinatario,
                Autor = processo.Autor,
                Processo = processo
            };

            _movimentacaoRepository.Add(movimento);

            return processo;
        }

        public Movimentacao Movimentar(Processo processo, Pessoa autor, Assignee destinatario, string parecer)
        {
            var movimento = new Movimentacao
            {
                Data = DateTime.Now,
                Origem = processo.Destinatario,
                Destino = destinatario,
                Autor= autor,
                Processo = processo,
                Parecer = parecer
            };

            _movimentacaoRepository.Add(movimento);

            return movimento;
        }
    }
}