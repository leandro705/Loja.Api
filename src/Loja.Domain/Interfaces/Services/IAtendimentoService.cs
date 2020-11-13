using Loja.CrossCutting.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Services
{
    public interface IAtendimentoService
    {        
        Task<ResultDto<IEnumerable<AtendimentoDto>>> ObterTodos(int? estabelecimentoId);
        Task<ResultDto<AtendimentoDto>> ObterPorId(int atendimentoId);
        Task<ResultDto<AtendimentoDto>> Create(AtendimentoDto atendimentoDto);
        Task<ResultDto<bool>> Update(AtendimentoDto atendimentoDto);
        Task<ResultDto<bool>> Delete(int atendimentoId);
        Task<ResultDto<bool>> FinalizarAtendimento(int atendimentoId);
        Task<ResultDto<int>> TotalAtendimentos(int? estabelecimentoId, string usuarioId, int? situacaoId);
        Task<ResultDto<List<TotalizadorMesDto>>> TotalAtendimentosMes(int? estabelecimentoId, string usuarioId, int? situacaoId);        
        Task<ResultDto<decimal>> ValorTotal(int? estabelecimentoId, string usuarioId, int? situacaoId);
    }
}
