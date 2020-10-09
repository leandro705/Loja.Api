using Loja.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Repository
{
    public interface IAgendamentoRepository : IRepository<Agendamento>
    {
        Task<IEnumerable<Agendamento>> ObterTodos(int? estabelecimentoId);
        Task<Agendamento> ObterPorId(int agendamentoId);
    }
}
