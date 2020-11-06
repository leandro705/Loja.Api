using Loja.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Repository
{
    public interface IAtendimentoRepository : IRepository<Atendimento>
    {
        Task<IEnumerable<Atendimento>> ObterTodos(int? estabelecimentoId);
        Task<Atendimento> ObterPorId(int atendimentoId);
        Task<int> ObterTotalAtendimentos(int? estabelecimentoId, string usuarioId, int? situacaoId);
        Task<int> ObterTotalAtendimentosMes(int? estabelecimentoId, string usuarioId, int? situacaoId, int mes, int ano);
        Task<decimal> ObterValorTotal(int? estabelecimentoId, string usuarioId, int? situacaoId);
        
    }
}
