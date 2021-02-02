using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.CrossCutting.Enumerators;
using Loja.Application.Validators;
using System;
using Loja.CrossCutting.Util;

namespace Loja.Application.Services
{
    public class AtendimentoService : IAtendimentoService
    {             
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMapper _mapper;

        public AtendimentoService(IAtendimentoRepository atendimentoRepository, IAgendamentoRepository agendamentoRepository, IMapper mapper)
        {
            _atendimentoRepository = atendimentoRepository;
            _agendamentoRepository = agendamentoRepository;
            _mapper = mapper;
        }

        public async Task<ResultDto<IEnumerable<AtendimentoDto>>> ObterTodos(int? estabelecimentoId)
        {
            var atendimentos = await _atendimentoRepository.ObterTodos(estabelecimentoId);

            if (!atendimentos.Any())
                return ResultDto<IEnumerable<AtendimentoDto>>.Validation("Atendimentos não encontrado na base de dados!");

            var atendimentoDto = _mapper.Map<IEnumerable<Atendimento>, IEnumerable<AtendimentoDto>>(atendimentos);

            return await Task.FromResult(ResultDto<IEnumerable<AtendimentoDto>>.Success(atendimentoDto));
        }

        public async Task<ResultDto<AtendimentoDto>> ObterPorId(int atendimentoId)
        {
            var atendimento = await _atendimentoRepository.ObterPorId(atendimentoId);

            if (atendimento == null)
                return await Task.FromResult(ResultDto<AtendimentoDto>.Validation("Atendimento não encontrado na base de dados!"));

            var atendimentoDto = _mapper.Map<Atendimento, AtendimentoDto>(atendimento);

            return await Task.FromResult(ResultDto<AtendimentoDto>.Success(atendimentoDto));
        }

        public async Task<ResultDto<AtendimentoDto>> Create(AtendimentoDto atendimentoDto)
        {
            var atendimentoDtoValidate = new AtendimentoDtoValidate(atendimentoDto);
            if (!atendimentoDtoValidate.Validate())
                return await Task.FromResult(ResultDto<AtendimentoDto>.Validation(atendimentoDtoValidate.Mensagens));

            var atendimento = _mapper.Map<Atendimento>(atendimentoDto);
            atendimento.SituacaoId = (int)ESituacao.PENDENTE;
            atendimento.DataCadastro = DateTime.Now;
            await _atendimentoRepository.Create(atendimento);

            if (atendimentoDto.AgendamentoId.HasValue)
            {
                var agendamento = await _agendamentoRepository.ObterPorId(atendimentoDto.AgendamentoId.Value);
                agendamento.FinalizarAgendamento();
                await _agendamentoRepository.Update(agendamento);
            }

            return await Task.FromResult(ResultDto<AtendimentoDto>.Success(_mapper.Map<AtendimentoDto>(atendimento)));
        }

        public async Task<ResultDto<bool>> Update(AtendimentoDto atendimentoDto)
        {
            var atendimentoDtoValidate = new AtendimentoDtoValidate(atendimentoDto);
            if (!atendimentoDtoValidate.Validate())
                return await Task.FromResult(ResultDto<bool>.Validation(atendimentoDtoValidate.Mensagens));

            var atendimento = await _atendimentoRepository.ObterPorId(atendimentoDto.AtendimentoId);
            atendimento.AtualizarAtendimento(atendimentoDto);

            var atendimentoItens = _mapper.Map<List<AtendimentoItem>>(atendimentoDto.AtendimentoItens);
            atendimento.AtualizarAtendimentoItens(atendimentoItens);

            await _atendimentoRepository.Update(atendimento);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<bool>> Delete(int atendimentoId)
        {
            var atendimento = await _atendimentoRepository.ObterPorId(atendimentoId);
            atendimento.CancelarAtendimento();
            await _atendimentoRepository.Update(atendimento);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<bool>> FinalizarAtendimento(int atendimentoId)
        {
            var atendimento = await _atendimentoRepository.ObterPorId(atendimentoId);
            atendimento.FinalizarAtendimento();
            await _atendimentoRepository.Update(atendimento);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<int>> TotalAtendimentos(int? estabelecimentoId, string usuarioId, int? situacaoId)
        {
            var total = await _atendimentoRepository.ObterTotalAtendimentos(estabelecimentoId, usuarioId, situacaoId);

            return await Task.FromResult(ResultDto<int>.Success(total));
        }

        public async Task<ResultDto<List<TotalizadorMesDto>>> TotalAtendimentosMes(int? estabelecimentoId, string usuarioId, int? situacaoId)
        {            
            var dataInicial = DateTime.Now.AddYears(-1);                                    
            var totalizadorMesDto = new List<TotalizadorMesDto>();
            for (int i = 0; i < 13; i++)            
            {
                var mes = dataInicial.Month;
                var ano = dataInicial.Year;
                totalizadorMesDto.Add(new TotalizadorMesDto()
                {
                    Total = await _atendimentoRepository.ObterTotalAtendimentosMes(estabelecimentoId, usuarioId, situacaoId, mes, ano),
                    Mes = ((EMeses)mes).GetDescription(),
                    Ano = ano
                });

                dataInicial = dataInicial.AddMonths(1);
            }
       
            return await Task.FromResult(ResultDto<List<TotalizadorMesDto>>.Success(totalizadorMesDto));
        }

        public async Task<ResultDto<decimal>> ValorTotal(int? estabelecimentoId, string usuarioId, int? situacaoId)
        {
            var total = await _atendimentoRepository.ObterValorTotal(estabelecimentoId, usuarioId, situacaoId);

            return await Task.FromResult(ResultDto<decimal>.Success(total));
        }
    }
}
