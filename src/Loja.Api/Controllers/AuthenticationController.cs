using Loja.Api.Configuration;
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
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public AuthenticationController(UserService userService,
            SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            _userService = userService;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
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

            var userDto = await _userService.Login(authDto);

            if (userDto != null)
                return ResultDto<AuthenticatedDto>.Success(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));

            return ResultDto<AuthenticatedDto>.Validation("Login ou senha inválidos!");
        }      

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<ResultDto<AuthenticatedDto>> AuthGoogle([FromBody] AuthDto authDto)
        {
            var authDtoValidador = new AuthDtoValidate(authDto);
            if (!authDtoValidador.Validate())
                return ResultDto<AuthenticatedDto>.Validation("Autenticação google inválida!");

            var userDto = await _userService.Login(authDto);

            if (userDto == null)
            {
                var result = await _userService.SalvarCliente(new UserDto()
                {
                    Email = authDto.Email,
                    Senha = authDto.Senha,
                    Nome = authDto.Nome,
                    IsGoogle = true
                });

                if (result.StatusCode != 200)
                    return ResultDto<AuthenticatedDto>.Validation(result.Errors);

                userDto = result.Data;
            }
            return ResultDto<AuthenticatedDto>.Success(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));
        }

        [AllowAnonymous]
        [HttpPost("facebook")]
        public async Task<ResultDto<AuthenticatedDto>> AuthFacebook([FromBody] AuthDto authDto)
        {
            var authDtoValidador = new AuthDtoValidate(authDto);
            if (!authDtoValidador.Validate())
                return ResultDto<AuthenticatedDto>.Validation("Autenticação facebook inválida!");

            var userDto = await _userService.Login(authDto);

            if (userDto == null)
            {
                var result = await _userService.SalvarCliente(new UserDto()
                {
                    Email = authDto.Email,
                    Senha = authDto.Senha,
                    Nome = authDto.Nome,
                    IsFacebook = true
                });

                if (result.StatusCode != 200)
                    return ResultDto<AuthenticatedDto>.Validation(result.Errors);

                userDto = result.Data;
            }
            return ResultDto<AuthenticatedDto>.Success(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));
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
            return await _userService.EnviarEmailRecuperarSenha(userDto.Email);           
        }

        [AllowAnonymous]
        [HttpGet("logoff")]
        public async Task LogOff()
        {
            await _userService.LogOff();
        }
    }
}
