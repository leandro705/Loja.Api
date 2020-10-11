using Loja.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Repository
{
    public interface IAtendimentoRepository : IRepository<Atendimento>
    {
        Task<IEnumerable<Atendimento>> ObterTodos(int? estabelecimentoId);
        Task<Atendimento> ObterPorId(int atendimentoId);
    }
}
