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
        public async Task<ResultDto<IEnumerable<ServicoDto>>> GetAll(int? estabelecimentoId)
        {
            return await _servicoService.ObterTodos(estabelecimentoId);
        }       

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultDto<ServicoDto>), 200)]
        public async Task<ResultDto<ServicoDto>> Get(int id)
        {
            return await _servicoService.ObterPorId(id);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(ResultDto<ServicoDto>), 200)]
        public async Task<ResultDto<ServicoDto>> Post([FromBody] ServicoDto servicoDto)
        {
            return await _servicoService.Create(servicoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Put(int id, [FromBody] ServicoDto servicoDto)
        {
            return await _servicoService.Update(servicoDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Delete(int id)
        {
            return await _servicoService.Delete(id);
        }
    }
}
