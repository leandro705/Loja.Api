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
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Administrador,Gerente,Cliente")]
        [HttpGet("clientes")]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<IEnumerable<UserDto>>> GetAll(int estabelecimentoId)
        {
            return await _userService.ObterTodosClientes(estabelecimentoId);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("")]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<IEnumerable<UserDto>>> GetAll(int? estabelecimentoId)
        {
            return await _userService.ObterTodos(estabelecimentoId);
        }

        [Authorize(Roles = "Administrador,Gerente,Cliente")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<UserDto>> Get(string id)
        {
            return await _userService.GetUserById(id);
        }

        [Authorize(Roles = "Administrador,Gerente,Cliente")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<UserDto>> Put(int id, [FromBody] UserDto userDto)
        {
            return await _userService.Update(userDto);
        }

        [Authorize(Roles = "Administrador,Gerente,Cliente")]
        [HttpPut]
        [Route("{id}/atualizar-senha")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> AtualizarSenha([FromBody] UserDto userDto)
        {
            return await _userService.AtualizarSenha(userDto);           
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost("")]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<UserDto>> Post([FromBody] UserDto userDto)
        {
            return await _userService.SalvarUsuario(userDto);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> Delete(string id)
        {
            return await _userService.Delete(id);
        }
    }
}
