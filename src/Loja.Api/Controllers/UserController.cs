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

        //[Authorize(Roles = "Administrador")]
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var user = await _userService.GetUserById(id);

        //    return Ok(user);
        //}
       
        //[Authorize(Roles = "Administrador")]
        //[HttpPost]
        //[ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        //public async Task<IActionResult> Post([FromBody] UserDto userDto)
        //{
        //    if (!userDto.IsValid())
        //        return BadRequest(userDto.ValidationMessages);

        //    var userCreated = await _userService.Create(userDto);

        //    if (userCreated != null)
        //        return Ok(new { message = $"Usuário {userCreated.Name} criado com sucesso!" });

        //    return BadRequest(new { message = "Falha ao criar usuário!" });
        //}

        //[Authorize(Roles = "Administrador")]
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
        //{
        //    var user = await _userService.Update(userDto);

        //    return Ok(new { message = $"Usuário {user.Name} atualizado com sucesso!" });
        //}

        //[Authorize(Roles = "Administrador")]
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _userService.Delete(id);

        //    return NoContent();
        //}



        //[Authorize(Roles = "PontoDeColeta,ProgramaDeReciclagem")]
        //[HttpPost]
        //[Route("atualizar-dados-usuario-logado")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> AtualizarDadosUsuarioLogado([FromBody] UserDto userDto)
        //{
        //    var user = await _userService.UpdateLoggedUserData(userDto);

        //    return Ok(new { message = $"Usuário {user.Name} atualizado com sucesso!" });
        //}

        ////[Authorize(Roles = "PontoDeColeta,ProgramaDeReciclagem")]
        //[HttpGet]
        //[Route("obter-dados-usuario-logado/{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> ObterDadosUsuarioLogado(Guid id)
        //{
        //    var user = await _userService.ObterDadosUsuarioLogado(id);

        //    return Ok(user);
        //}

        //[Authorize(Roles = "PontoDeColeta,ProgramaDeReciclagem")]
        //[HttpPost]
        //[Route("atualizar-senha")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> AtualizarSenha([FromBody] UserDto userDto)
        //{
        //    var updated = await _userService.AtualizarSenha(userDto);

        //    return Ok(new { updated = updated });
        //}        
    }
}
