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
    }
}
