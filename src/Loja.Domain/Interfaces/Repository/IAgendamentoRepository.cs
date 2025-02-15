﻿using Loja.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Repository
{
    public interface IAgendamentoRepository : IRepository<Agendamento>
    {
        Task<IEnumerable<Agendamento>> ObterTodosCalendario(DateTime inicio, DateTime final, int? estabelecimentoId, string usuarioId);
        Task<IEnumerable<Agendamento>> ObterTodos(int? estabelecimentoId);
        Task<Agendamento> ObterPorId(int agendamentoId);
        Task<int> ObterTotalAgendamentos(int? estabelecimentoId, string usuarioId);
        Task<IEnumerable<Agendamento>> ObterAgendamentosPorEstabelecimentoEData(DateTime dataAgendamento, int estabelecimentoId);
        Task<bool> ValidaAgendamentoDuplicados(int estabelecimentoId, int agendamentoId, DateTime dataAgendamento, DateTime dataFinalAgendamento);
    }
}
