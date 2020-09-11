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
        public async Task<ResultDto<object>> Auth([FromBody] AuthDto authDto)
        {
            var authDtoValidador = new AuthDtoValidate(authDto);
            if (!authDtoValidador.Validate())
                return ResultDto<object>.Validation(authDtoValidador.Mensagens);

            var userDto = await _userService.Login(authDto);

            if (userDto != null)
                return ResultDto<object>.Success(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));

            return ResultDto<object>.Validation("Login ou senha inválidos!");
        }       

        //[AllowAnonymous]
        //[HttpPost("google")]
        //public async Task<IActionResult> AuthGoogle([FromBody] AuthDto authDto)
        //{
        //    var payload = GoogleJsonWebSignature.ValidateAsync(authDto.Token, new GoogleJsonWebSignature.ValidationSettings()).Result;

        //    if (!payload.EmailVerified)
        //        return BadRequest(new List<string> { "Login inválido!" });

        //    var userDto = await _userService.LoginSocial(payload.Email);

        //    var isClientValid = false;

        //    if (userDto != null && (userDto.Roles.Contains("ProgramaDeReciclagem") || userDto.Roles.Contains("PontoDeColeta")))
        //    {
        //        isClientValid = true;
        //        userDto.IsGoogle = true;
        //    }

        //    if (userDto != null && isClientValid)
        //        return Ok(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));

        //    return Unauthorized(new { authenticated = false, message = $"E-mail {authDto.Email} não cadastrado na plataforma!" });
        //}

        //[AllowAnonymous]
        //[HttpPost("facebook")]
        //public async Task<IActionResult> AuthFacebook([FromBody] AuthDto authDto)
        //{
        //    if (string.IsNullOrEmpty(authDto.Email))
        //        return BadRequest(new List<string> { "Login inválido!" });

        //    var userDto = await _userService.LoginSocial(authDto.Email);

        //    var isClientValid = false;

        //    if (userDto != null && (userDto.Roles.Contains("ProgramaDeReciclagem") || userDto.Roles.Contains("PontoDeColeta")))
        //    {
        //        isClientValid = true;
        //        userDto.IsFacebook = true;
        //    }

        //    if (userDto != null && isClientValid)
        //        return Ok(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));

        //    return Unauthorized(new { authenticated = false, message = "Login ou senha inválidos!" });
        //}

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            return await _userService.ConfirmEmail(userId, token);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<bool> ResetPassword(ResetPassowordDto dto)
        {
            return await _userService.ResetPassword(dto);
        }

        [AllowAnonymous]
        [HttpGet("log-off")]
        public async Task LogOff()
        {
            await _userService.LogOff();
        }
    }
}
