using Loja.Application.Services;
using Loja.CrossCutting.Dto;
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
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("clientes")]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<IEnumerable<UserDto>>> GetAll(int estabelecimentoId)
        {
            return await _userService.ObterTodosClientes(estabelecimentoId);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<UserDto>> Get(string id)
        {
            return await _userService.GetUserById(id);
        }        
        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<UserDto>> Put(int id, [FromBody] UserDto userDto)
        {
            return await _userService.Update(userDto);
        }

        [HttpPut]
        [Route("{id}/atualizar-senha")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> AtualizarSenha([FromBody] UserDto userDto)
        {
            return await _userService.AtualizarSenha(userDto);           
        }
    }
}
