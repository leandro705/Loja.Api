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
    public class EstabelecimentoRepository : Repository<Estabelecimento>, IEstabelecimentoRepository
    {
        public EstabelecimentoRepository(LojaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Estabelecimento>> ObterTodos(string url)
        {
            return await _context.Set<Estabelecimento>()
                .Where(x =>  (string.IsNullOrEmpty(url) || x.Url == url))
                .Where(x => x.SituacaoId == (int)ESituacao.ATIVO)
                .Include(x => x.Situacao)
                .ToListAsync();
        }

        public async Task<Estabelecimento> ObterPorId(int estabelecimentoId)
        {
            return await _context.Set<Estabelecimento>()
                .Where(x => x.EstabelecimentoId == estabelecimentoId)
                .Include(x => x.Situacao)                    
                .FirstOrDefaultAsync();
        }
    }
}
