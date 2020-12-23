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
    [Route("api/estabelecimentos")]
    [Authorize]
    public class EstabelecimentoController : Controller
    {
        private readonly IEstabelecimentoService _estabelecimentoService;

        public EstabelecimentoController(IEstabelecimentoService estabelecimentoService)
        {
            _estabelecimentoService = estabelecimentoService;
        }

        [AllowAnonymous]
        [HttpGet("{url}/dadosLogin")]
        [ProducesResponseType(typeof(ResultDto<EstabelecimentoDto>), 200)]
        public async Task<ResultDto<EstabelecimentoDto>> ObterPorUrl(string url)
        {
            return await _estabelecimentoService.ObterPorUrl(url);
        }

        [Authorize(Roles = "Administrador,Gerente")]
        [HttpGet("")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<EstabelecimentoDto>>), 200)]
        public async Task<ResultDto<IEnumerable<EstabelecimentoDto>>> GetAll(string url)
        {
            return await _estabelecimentoService.ObterTodos(url);
        }

        [Authorize(Roles = "Administrador,Gerente,Cliente")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultDto<EstabelecimentoDto>), 200)]
        public async Task<ResultDto<EstabelecimentoDto>> Get(int id)
        {
            return await _estabelecimentoService.ObterPorId(id);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("")]
        [ProducesResponseType(typeof(ResultDto<EstabelecimentoDto>), 200)]
        public async Task<ResultDto<EstabelecimentoDto>> Post([FromBody] EstabelecimentoDto estabelecimentoDto)
        {
            return await _estabelecimentoService.Create(estabelecimentoDto);
        }

        [Authorize(Roles = "Administrador,Gerente")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Put(int id, [FromBody] EstabelecimentoDto estabelecimentoDto)
        {
            return await _estabelecimentoService.Update(estabelecimentoDto);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Delete(int id)
        {
            return await _estabelecimentoService.Delete(id);
        }
    }
}
