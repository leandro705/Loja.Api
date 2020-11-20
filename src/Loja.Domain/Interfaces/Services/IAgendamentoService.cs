using Loja.CrossCutting.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Services
{
    public interface IAgendamentoService
    {        
        Task<ResultDto<IEnumerable<AgendamentoDto>>> ObterTodosCalendario(DateTime inicio, DateTime final, int? estabelecimentoId, string usuarioId);
        Task<ResultDto<IEnumerable<AgendamentoDto>>> ObterTodos(int? estabelecimentoId);
        Task<ResultDto<AgendamentoDto>> ObterPorId(int id);
        Task<ResultDto<AgendamentoDto>> Create(AgendamentoDto agendamentoDto);
        Task<ResultDto<bool>> Update(AgendamentoDto agendamentoDto);
        Task<ResultDto<bool>> Delete(int servicoId);
        Task<ResultDto<int>> TotalAgendamentos(int? estabelecimentoId, string usuarioId);
        Task<ResultDto<IEnumerable<HorarioDisponivelDto>>> ObterHorariosDisponiveis(string dataAgendamento, int estabelecimentoId, int servicoId);
    }
}
