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
    public class AtendimentoService : IAtendimentoService
    {             
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IMapper _mapper;

        public AtendimentoService(IAtendimentoRepository atendimentoRepository, IMapper mapper)
        {
            _atendimentoRepository = atendimentoRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<AtendimentoDto>>> ObterTodos()
        {
            var atendimentos = await _atendimentoRepository.GetAll();

            if (!atendimentos.Any())
                return ResultDto<IEnumerable<AtendimentoDto>>.Validation("Atendimentos não encontrado na base de dados!");

            var atendimentoDto = _mapper.Map<IEnumerable<Atendimento>, IEnumerable<AtendimentoDto>>(atendimentos);          

            return await Task.FromResult(ResultDto<IEnumerable<AtendimentoDto>>.Success(atendimentoDto));
        }
    }
}
