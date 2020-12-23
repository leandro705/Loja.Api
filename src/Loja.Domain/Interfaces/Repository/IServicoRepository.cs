using Loja.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Repository
{
    public interface IServicoRepository : IRepository<Servico>
    {
        Task<IEnumerable<Servico>> ObterTodos(int? estabelecimentoId);
        Task<IEnumerable<Servico>> ObterTodosAtivos(int? estabelecimentoId);
        Task<Servico> ObterPorId(int servicoId);
    }
}
