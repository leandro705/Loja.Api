using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class AgendamentoService : IAgendamentoService
    {             
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMapper _mapper;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IMapper mapper)
        {
            _agendamentoRepository = agendamentoRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<AgendamentoDto>>> ObterTodos()
        {
            var agendamentos = await _agendamentoRepository.GetAll();

            if (!agendamentos.Any())
                return ResultDto<IEnumerable<AgendamentoDto>>.Validation("Agendamentos não encontrado na base de dados!");

            var agendamentoDto = _mapper.Map<IEnumerable<Agendamento>, IEnumerable<AgendamentoDto>>(agendamentos);          

            return await Task.FromResult(ResultDto<IEnumerable<AgendamentoDto>>.Success(agendamentoDto));
        }     
    }
}
