using Loja.Application.Services;
using Loja.CrossCutting.Dto;
using Loja.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Api.Controllers
{
    [EnableCors("ApiCorsPolicy")]    
    [Route("api/agendamentos")]
    public class AgendamentoController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpGet("calendario")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<AgendamentoDto>>), 200)]
        public async Task<ResultDto<IEnumerable<AgendamentoDto>>> GetCalendario(DateTime inicio, DateTime final, int? estabelecimentoId, string usuarioId)
        {
            return await _agendamentoService.ObterTodosCalendario(inicio, final, estabelecimentoId, usuarioId);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<AgendamentoDto>>), 200)]
        public async Task<ResultDto<IEnumerable<AgendamentoDto>>> GetAll(int? estabelecimentoId)
        {
            return await _agendamentoService.ObterTodos(estabelecimentoId);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultDto<AgendamentoDto>), 200)]
        public async Task<ResultDto<AgendamentoDto>> Get(int id)
        {
            return await _agendamentoService.ObterPorId(id);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(ResultDto<AgendamentoDto>), 200)]
        public async Task<ResultDto<AgendamentoDto>> Post([FromBody] AgendamentoDto agendamentoDto)
        {
            return await _agendamentoService.Create(agendamentoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Put(int id, [FromBody] AgendamentoDto agendamentoDto)
        {
            return await _agendamentoService.Update(agendamentoDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Delete(int id)
        {
            return await _agendamentoService.Delete(id);
        }
    }
}
