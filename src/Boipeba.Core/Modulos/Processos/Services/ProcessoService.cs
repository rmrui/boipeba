﻿using System;
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
        private readonly ITicketProvider _ticketProvider;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly GlobalSettings _globalSettings;

        public ProcessoService(IProcessoMovimentoRepository processoMovimentoRepository, IProcessoRepository processoRepository, ITicketProvider ticketProvider, IPessoaRepository pessoaRepository, IMovimentoRepository movimentoRepository, GlobalSettings globalSettings)
        {
            _processoMovimentoRepository = processoMovimentoRepository;
            _processoRepository = processoRepository;
            _ticketProvider = ticketProvider;
            _pessoaRepository = pessoaRepository;
            _movimentoRepository = movimentoRepository;
            _globalSettings = globalSettings;
        }

        public Processo Cadastrar(Processo processo)
        {
            _processoRepository.Add(processo);
            var usuarioAutor = _ticketProvider.ResolveUser();
            var autor = _pessoaRepository.Find(usuarioAutor.Matricula);

            var processoMovimentoEncaminhamento = new ProcessoMovimento
            {
                Data = DateTime.Now,
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
                Parecer = parecer
            };

            _processoMovimentoRepository.Add(movimento);

            return movimento;
        }
    }
}