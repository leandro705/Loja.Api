using Loja.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Repository
{
    public interface IEstabelecimentoRepository : IRepository<Estabelecimento>
    {
        Task<IEnumerable<Estabelecimento>> ObterTodos();
        Task<Estabelecimento> ObterPorId(int estabelecimentoId);
    }
}
