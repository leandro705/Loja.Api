using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Repository.Context;

namespace Loja.Repository.Repositories
{
    public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
    {
        public AtendimentoRepository(LojaDbContext context) : base(context)
        {
        }
    }
}
