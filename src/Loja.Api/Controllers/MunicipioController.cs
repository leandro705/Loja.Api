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
    [Route("api/municipios")]
    [Authorize]
    public class MunicipioController : Controller
    {
        private readonly IMunicipioService _municipioService;

        public MunicipioController(IMunicipioService municipioService)
        {
            _municipioService = municipioService;
        }

        [Authorize(Roles = "Administrador,Gerente,Cliente")]
        [HttpGet("estado/{id}")]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<MunicipioDto>>), 200)]
        public async Task<ResultDto<IEnumerable<MunicipioDto>>> Get(int id)
        {
            return await _municipioService.ObterTodosPorEstado(id);
        } 
    }
}
