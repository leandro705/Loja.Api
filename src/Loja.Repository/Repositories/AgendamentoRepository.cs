using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Repository.Context;

namespace Loja.Repository.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(LojaDbContext context) : base(context)
        {
        }
    }
}
