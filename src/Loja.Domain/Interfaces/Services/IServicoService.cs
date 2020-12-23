using Loja.CrossCutting.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Services
{
    public interface IServicoService
    {
        Task<ResultDto<IEnumerable<ServicoDto>>> ObterTodos(int? estabelecimentoId);
        Task<ResultDto<IEnumerable<ServicoDto>>> ObterTodosAtivos(int? estabelecimentoId);
        Task<ResultDto<ServicoDto>> ObterPorId(int id);
        Task<ResultDto<ServicoDto>> Create(ServicoDto servicoDto);
        Task<ResultDto<bool>> Update(ServicoDto servicoDto);
        Task<ResultDto<bool>> Delete(int servicoId);
        Task<ResultDto<bool>> Ativar(int servicoId);
        Task<ResultDto<bool>> Desativar(int servicoId);
    }
}
