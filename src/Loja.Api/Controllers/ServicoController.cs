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
    [Route("api/servicos")]
    public class ServicoController : Controller
    {
        private readonly IServicoService _servicoService;

        public ServicoController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }
        
        [HttpGet("")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<ServicoDto>>), 200)]
        public async Task<ResultDto<IEnumerable<ServicoDto>>> Get()
        {
            return await _servicoService.ObterTodos();
        } 
    }
}
