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
                .Where(x => x.SituacaoId == (int)ESituacao.ATIVO && x.DataAgendamento.Date >= inicio.Date && x.DataAgendamento.Date <= final.Date)
                .Include(x => x.Situacao)
                .Include(x => x.Servico)
                .Include(x => x.Usuario)
                .Include(x => x.Estabelecimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Agendamento>> ObterTodos(int? estabelecimentoId)
        {
            return await _context.Set<Agendamento>()
                .Where(x => (!estabelecimentoId.HasValue || x.EstabelecimentoId == estabelecimentoId))
                .Where(x => x.SituacaoId == (int)ESituacao.ATIVO)
                .Include(x => x.Situacao)
                .Include(x => x.Servico)
                .Include(x => x.Usuario)
                .Include(x => x.Estabelecimento)
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
                .FirstOrDefaultAsync();
        }
    }
}
