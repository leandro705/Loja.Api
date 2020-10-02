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
        
        [HttpGet("")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<AgendamentoDto>>), 200)]
        public async Task<ResultDto<IEnumerable<AgendamentoDto>>> Get()
        {
            return await _agendamentoService.ObterTodos();
        } 
    }
}
