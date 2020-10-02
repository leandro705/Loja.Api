using Loja.CrossCutting.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Domain.Interfaces.Services
{
    public interface IAtendimentoService
    {
        Task<ResultDto<IEnumerable<AtendimentoDto>>> ObterTodos();
    }
}
