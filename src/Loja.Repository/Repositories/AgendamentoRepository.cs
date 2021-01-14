using Loja.CrossCutting.Enumerators;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Repository.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(LojaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Agendamento>> ObterTodosCalendario(DateTime inicio, DateTime final, int? estabelecimentoId, string usuarioId)
        {
            return await _context.Set<Agendamento>()
                .Where(x => (!estabelecimentoId.HasValue || x.EstabelecimentoId == estabelecimentoId))
                .Where(x => (string.IsNullOrEmpty(usuarioId) || x.UserId == usuarioId))                
                .Where(x => x.SituacaoId != (int)ESituacao.CANCELADO && x.DataAgendamento.Date >= inicio.Date && x.DataAgendamento.Date <= final.Date)
                .Include(x => x.Situacao)
                .Include(x => x.Servico)
                .Include(x => x.Usuario)
                .Include(x => x.Estabelecimento)
                .Include(x => x.Atendimentos)
                .ToListAsync();
        }

        public async Task<IEnumerable<Agendamento>> ObterTodos(int? estabelecimentoId)
        {
            return await _context.Set<Agendamento>()
                .Where(x => (!estabelecimentoId.HasValue || x.EstabelecimentoId == estabelecimentoId))                
                .Include(x => x.Situacao)
                .Include(x => x.Servico)
                .Include(x => x.Usuario)
                .Include(x => x.Estabelecimento)
                .Include(x => x.Atendimentos)
                .ToListAsync();
        }

        public async Task<Agendamento> ObterPorId(int agendamentoId)
        {
            return await _context.Set<Agendamento>()
                .Where(x => x.AgendamentoId == agendamentoId)
                .Include(x => x.Situacao)
                .Include(x => x.Servico)
                .Include(x => x.Usuario)
                .Include(x => x.Estabelecimento)
                .Include(x => x.Atendimentos)
                .FirstOrDefaultAsync();
        }

        public async Task<int> ObterTotalAgendamentos(int? estabelecimentoId, string usuarioId)
        {
            var totalCadastrado = await _context.Set<Agendamento>()                 
                   .CountAsync(x => (!estabelecimentoId.HasValue || x.EstabelecimentoId == estabelecimentoId) &&
                   (string.IsNullOrEmpty(usuarioId) || x.UserId == usuarioId));

            return await Task.FromResult(totalCadastrado);
        }

        public async Task<IEnumerable<Agendamento>> ObterAgendamentosPorEstabelecimentoEData(DateTime dataAgendamento, int estabelecimentoId)
        {
            return await _context.Set<Agendamento>()
                .Where(x => x.EstabelecimentoId == estabelecimentoId && x.SituacaoId != (int)ESituacao.CANCELADO && x.DataAgendamento.Date == dataAgendamento.Date)
                .Include(x => x.Servico)
                .ToListAsync();
        }

        public async Task<bool> ValidaAgendamentoDuplicados(Agendamento agendamento)
        {
            return await _context.Set<Agendamento>()
                .AnyAsync(x => 
                    x.EstabelecimentoId == agendamento.EstabelecimentoId && x.SituacaoId != (int)ESituacao.CANCELADO &&
                    ((agendamento.AgendamentoId > 0 && x.AgendamentoId != agendamento.AgendamentoId) || agendamento.AgendamentoId == 0) &&
                    ((agendamento.DataAgendamento >= x.DataAgendamento && agendamento.DataAgendamento < x.DataFinalAgendamento) || 
                    (agendamento.DataFinalAgendamento > x.DataAgendamento && agendamento.DataFinalAgendamento <= x.DataFinalAgendamento))
                );
                
        }
    }
}
