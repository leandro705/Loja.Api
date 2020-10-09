﻿using Loja.CrossCutting.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Services
{
    public interface IAgendamentoService
    {        
        Task<ResultDto<IEnumerable<AgendamentoDto>>> ObterTodos(int? estabelecimentoId);
        Task<ResultDto<AgendamentoDto>> ObterPorId(int id);
        Task<ResultDto<AgendamentoDto>> Create(AgendamentoDto agendamentoDto);
        Task<ResultDto<bool>> Update(AgendamentoDto agendamentoDto);
        Task<ResultDto<bool>> Delete(int servicoId);
    }
}
