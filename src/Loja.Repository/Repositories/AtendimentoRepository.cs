using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Repository.Repositories
{
    public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
    {
        public AtendimentoRepository(LojaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Atendimento>> ObterTodos(int? estabelecimentoId)
        {
            return await _context.Set<Atendimento>()
                .Where(x => (!estabelecimentoId.HasValue || x.EstabelecimentoId == estabelecimentoId))                
                .Include(x => x.Situacao)
                .Include(x => x.Usuario)
                .Include(x => x.Estabelecimento)
                .ToListAsync();
        }

        public async Task<Atendimento> ObterPorId(int atendimentoId)
        {
            return await _context.Set<Atendimento>()
                .Where(x => x.AtendimentoId == atendimentoId)
                .Include(x => x.Situacao)
                .Include(x => x.AtendimentoItens)
                    .ThenInclude(x => x.Servico)
                .FirstOrDefaultAsync();
        }
    }
}
