﻿using Loja.Application.Services;
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
    [Route("api/atendimentos")]
    [Authorize]
    public class AtendimentoController : Controller
    {
        private readonly IAtendimentoService _atendimentoService;

        public AtendimentoController(IAtendimentoService atendimentoService)
        {
            _atendimentoService = atendimentoService;
        }

        [Authorize(Roles = "Administrador,Gerente")]
        [HttpGet("")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<AtendimentoDto>>), 200)]
        public async Task<ResultDto<IEnumerable<AtendimentoDto>>> GetAll(int? estabelecimentoId)
        {
            return await _atendimentoService.ObterTodos(estabelecimentoId);
        }

        [Authorize(Roles = "Administrador,Gerente")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultDto<AtendimentoDto>), 200)]
        public async Task<ResultDto<AtendimentoDto>> Get(int id)
        {
            return await _atendimentoService.ObterPorId(id);
        }

        [Authorize(Roles = "Administrador,Gerente")]
        [HttpPost("")]
        [ProducesResponseType(typeof(ResultDto<AtendimentoDto>), 200)]
        public async Task<ResultDto<AtendimentoDto>> Post([FromBody] AtendimentoDto atendimentoDto)
        {
            return await _atendimentoService.Create(atendimentoDto);
        }

        [Authorize(Roles = "Administrador,Gerente")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Put(int id, [FromBody] AtendimentoDto atendimentoDto)
        {
            return await _atendimentoService.Update(atendimentoDto);
        }

        [Authorize(Roles = "Administrador,Gerente")]
        [HttpPut("{id}/finalizar")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> FinalizarAtendimento(int id)
        {
            return await _atendimentoService.FinalizarAtendimento(id);
        }

        [Authorize(Roles = "Administrador,Gerente")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Delete(int id)
        {
            return await _atendimentoService.Delete(id);
        }
    }
}
