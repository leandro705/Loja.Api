using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class ServicoService : IServicoService
    {             
        private readonly IServicoRepository _servicoRepository;
        private readonly IMapper _mapper;

        public ServicoService(IServicoRepository servicoRepository, IMapper mapper)
        {
            _servicoRepository = servicoRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<ServicoDto>>> ObterTodos()
        {
            var servicos = await _servicoRepository.GetAll();

            if (!servicos.Any())
                return ResultDto<IEnumerable<ServicoDto>>.Validation("Serviços não encontrado na base de dados!");

            var servicoDto = _mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoDto>>(servicos);          

            return await Task.FromResult(ResultDto<IEnumerable<ServicoDto>>.Success(servicoDto));
        }     
    }
}
