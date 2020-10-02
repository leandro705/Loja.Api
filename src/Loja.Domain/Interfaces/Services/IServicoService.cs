using Loja.CrossCutting.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Services
{
    public interface IServicoService
    {
        Task<ResultDto<IEnumerable<ServicoDto>>> ObterTodos();
    }
}
