using Loja.Application.Services;
using Loja.Application.Validators;
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
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService _userService;       

        public AuthenticationController(UserService userService)
        {
            _userService = userService;            
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ResultDto<UserDto>), 200)]
        public async Task<ResultDto<UserDto>> Post([FromBody] UserDto userDto)
        {
            return await _userService.SalvarCliente(userDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ResultDto<AuthenticatedDto>> Auth([FromBody] AuthDto authDto)
        {
            var authDtoValidador = new AuthDtoValidate(authDto);
            if (!authDtoValidador.Validate())
                return ResultDto<AuthenticatedDto>.Validation(authDtoValidador.Mensagens);

            return await _userService.Login(authDto);           
        }

        [AllowAnonymous]
        [HttpPost("loginAdmin")]
        public async Task<ResultDto<AuthenticatedDto>> AuthAdmin([FromBody] AuthDto authDto)
        {
            var authDtoAdminValidador = new AuthDtoAdminValidate(authDto);
            if (!authDtoAdminValidador.Validate())
                return ResultDto<AuthenticatedDto>.Validation(authDtoAdminValidador.Mensagens);

            return await _userService.LoginAdmin(authDto);            
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<ResultDto<AuthenticatedDto>> AuthGoogle([FromBody] AuthDto authDto)
        {
            var authDtoValidador = new AuthDtoValidate(authDto);
            if (!authDtoValidador.Validate())
                return ResultDto<AuthenticatedDto>.Validation("Autenticação google inválida!");

            authDto.IsGoogle = true;
            authDto.IsFacebook = false;
            return await _userService.LoginSocial(authDto);
        }

        [AllowAnonymous]
        [HttpPost("facebook")]
        public async Task<ResultDto<AuthenticatedDto>> AuthFacebook([FromBody] AuthDto authDto)
        {
            var authDtoValidador = new AuthDtoValidate(authDto);
            if (!authDtoValidador.Validate())
                return ResultDto<AuthenticatedDto>.Validation("Autenticação facebook inválida!");

            authDto.IsGoogle = false;
            authDto.IsFacebook = true;
            return await _userService.LoginSocial(authDto);            
        }
      
        [AllowAnonymous]
        [HttpPost("recuperar-senha")]
        public async Task<ResultDto<bool>> RecuperarSenha(RecuperarSenhaDto dto)
        {
            return await _userService.RecuperarSenha(dto);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("enviar-email-recuperacao-senha")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public async Task<ResultDto<bool>> EnviarEmailDeRecuperacaoDeSenha([FromBody] UserDto userDto)
        {
            return await _userService.EnviarEmailRecuperarSenha(userDto.Email, userDto.EstabelecimentoId);           
        }

        [AllowAnonymous]
        [HttpGet("logoff")]
        public async Task LogOff()
        {
            await _userService.LogOff();
        }
    }
}
