using Loja.CrossCutting.Enumerators;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Repository.Repositories
{
    public class ServicoRepository : Repository<Servico>, IServicoRepository
    {
        public ServicoRepository(LojaDbContext context) : base(context)
        {            
        }

        public async Task<IEnumerable<Servico>> ObterTodos(int? estabelecimentoId)
        {
            return await _context.Set<Servico>()
                .Where(x => (!estabelecimentoId.HasValue || x.EstabelecimentoId == estabelecimentoId))
                .Where(x => x.SituacaoId != (int)ESituacao.CANCELADO)
                .Include(x => x.Situacao)
                .Include(x => x.Estabelecimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Servico>> ObterTodosAtivos(int? estabelecimentoId)
        {
            return await _context.Set<Servico>()
                .Where(x => (!estabelecimentoId.HasValue || x.EstabelecimentoId == estabelecimentoId))
                .Where(x => x.SituacaoId == (int)ESituacao.ATIVO)
                .Include(x => x.Situacao)
                .Include(x => x.Estabelecimento)
                .ToListAsync();
        }

        public async Task<Servico> ObterPorId(int servicoId)
        {
            return await _context.Set<Servico>()
                .Where(x => x.ServicoId == servicoId)
                .Include(x => x.Situacao)
                .FirstOrDefaultAsync();
        }
    }
}
