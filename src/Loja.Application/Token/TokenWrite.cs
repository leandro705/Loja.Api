using Loja.CrossCutting.Dto;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Loja.Application
{
    public class TokenWrite
    {
        public static AuthenticatedDto WriteToken(UserDto userDto, TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations)
        {
            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = CreateClaimsIdentity(userDto),
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            };

            var securityToken = handler.CreateToken(tokenDescriptor);

            var token = handler.WriteToken(securityToken);

            return new AuthenticatedDto()
            {
                Id = userDto.Id,
                Nome = userDto.Nome,
                Email = userDto.Email,
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Role = userDto?.Role,
                IsGoogle = userDto.IsGoogle,
                IsFacebook = userDto.IsFacebook,
                EstabelecimentoId = userDto.EstabelecimentoId,
                EstabelecimentoNomeUrl = userDto.EstabelecimentoNomeUrl,
                EstabelecimentoNome = userDto.EstabelecimentoNome,
                IsAdministrador = userDto.Role == "Administrador"
            };
        }

        private static ClaimsIdentity CreateClaimsIdentity(UserDto userDto)
        {
            return new ClaimsIdentity(new GenericIdentity(userDto.Nome, "Login"), PrepareClaims(userDto));
        }

        private static List<Claim> PrepareClaims(UserDto userDto)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Email, userDto.Email),
                new Claim(ClaimTypes.Role, userDto.Role)
            };

            return claims;
        }
    }
}
