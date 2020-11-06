using AutoMapper;
using Loja.Application.Validators;
using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class AgendamentoService : IAgendamentoService
    {             
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMapper _mapper;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IMapper mapper)
        {
            _agendamentoRepository = agendamentoRepository;
            _mapper = mapper;
        }       

        public async Task<ResultDto<IEnumerable<AgendamentoDto>>> ObterTodosCalendario(DateTime inicio, DateTime final, int? estabelecimentoId, string usuarioId)
        {
            var agendamentos = await _agendamentoRepository.ObterTodosCalendario(inicio, final, estabelecimentoId, usuarioId);

            if (!agendamentos.Any())
                return ResultDto<IEnumerable<AgendamentoDto>>.Validation("Agendamentos não encontrado na base de dados!");

            var agendamentoDto = _mapper.Map<IEnumerable<Agendamento>, IEnumerable<AgendamentoDto>>(agendamentos);

            return await Task.FromResult(ResultDto<IEnumerable<AgendamentoDto>>.Success(agendamentoDto));
        }

        public async Task<ResultDto<IEnumerable<AgendamentoDto>>> ObterTodos(int? estabelecimentoId)
        {
            var agendamentos = await _agendamentoRepository.ObterTodos(estabelecimentoId);

            if (!agendamentos.Any())
                return ResultDto<IEnumerable<AgendamentoDto>>.Validation("Agendamentos não encontrado na base de dados!");

            var agendamentoDto = _mapper.Map<IEnumerable<Agendamento>, IEnumerable<AgendamentoDto>>(agendamentos);

            return await Task.FromResult(ResultDto<IEnumerable<AgendamentoDto>>.Success(agendamentoDto));
        }

        public async Task<ResultDto<AgendamentoDto>> ObterPorId(int agendamentoId)
        {
            var agendamento = await _agendamentoRepository.ObterPorId(agendamentoId);

            if (agendamento == null)
                return await Task.FromResult(ResultDto<AgendamentoDto>.Validation("Agendamento não encontrado na base de dados!"));

            var agendamentoDto = _mapper.Map<Agendamento, AgendamentoDto>(agendamento);

            return await Task.FromResult(ResultDto<AgendamentoDto>.Success(agendamentoDto));
        }

        public async Task<ResultDto<AgendamentoDto>> Create(AgendamentoDto agendamentoDto)
        {
            var agendamentoDtoValidate = new AgendamentoDtoValidate(agendamentoDto);
            if (!agendamentoDtoValidate.Validate())
                return await Task.FromResult(ResultDto<AgendamentoDto>.Validation(agendamentoDtoValidate.Mensagens));

            var agendamento = _mapper.Map<Agendamento>(agendamentoDto);
            agendamento.SituacaoId = (int)ESituacao.ATIVO;
            agendamento.DataCadastro = DateTime.Now;
            await _agendamentoRepository.Create(agendamento);
            return await Task.FromResult(ResultDto<AgendamentoDto>.Success(_mapper.Map<AgendamentoDto>(agendamento)));
        }

        public async Task<ResultDto<bool>> Update(AgendamentoDto agendamentoDto)
        {
            var agendamentoDtoValidate = new AgendamentoDtoValidate(agendamentoDto);
            if (!agendamentoDtoValidate.Validate())
                return await Task.FromResult(ResultDto<bool>.Validation(agendamentoDtoValidate.Mensagens));

            var agendamento = await _agendamentoRepository.ObterPorId(agendamentoDto.AgendamentoId);
            agendamento.AtualizarAgendamento(agendamentoDto);
            await _agendamentoRepository.Update(agendamento);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<bool>> Delete(int agendamentoId)
        {
            var agendamento = await _agendamentoRepository.ObterPorId(agendamentoId);
            agendamento.DesabilitarAgendamento();
            await _agendamentoRepository.Update(agendamento);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<int>> TotalAgendamentos(int? estabelecimentoId, string usuarioId)
        {
            var totalCadastrado = await _agendamentoRepository.ObterTotalAgendamentos(estabelecimentoId, usuarioId);

            return await Task.FromResult(ResultDto<int>.Success(totalCadastrado));
        }        
    }
}
