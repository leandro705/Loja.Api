using Loja.CrossCutting.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Services
{
    public interface IEstabelecimentoService
    {
        Task<ResultDto<IEnumerable<EstabelecimentoDto>>> ObterTodos();
        Task<ResultDto<EstabelecimentoDto>> ObterPorId(int id);
        Task<ResultDto<EstabelecimentoDto>> Create(EstabelecimentoDto estabelecimentoDto);
        Task<ResultDto<bool>> Update(EstabelecimentoDto estabelecimentoDto);
        Task<ResultDto<bool>> Delete(int estabelecimentoId);
    }
}
