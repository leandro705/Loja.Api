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
    [Route("api/estados")]
    [Authorize]
    public class EstadoController : Controller
    {
        private readonly IEstadoService _estadoService;

        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [Authorize(Roles = "Administrador,Gerente,Cliente")]
        [HttpGet("")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<EstadoDto>>), 200)]
        public async Task<ResultDto<IEnumerable<EstadoDto>>> Get()
        {
            return await _estadoService.ObterTodos();
        } 
    }
}
