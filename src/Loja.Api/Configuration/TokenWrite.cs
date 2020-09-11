using Loja.CrossCutting.Dto;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Loja.Api.Configuration
{
    public class TokenWrite
    {
        public static object WriteToken(UserDto userDto, TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations)
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

            return new
            {
                id = userDto.Id,
                userName = userDto.Nome,
                email = userDto.Email,
                authenticated = true,
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                isGooogle = userDto.IsGoogle,
                message = "OK",
                role = userDto?.Roles?.FirstOrDefault()
            };
        }

        private static ClaimsIdentity CreateClaimsIdentity(UserDto userDto)
        {
            return new ClaimsIdentity(new GenericIdentity(userDto.Nome, "Login"), PrepareClaims(userDto));
        }

        private static List<Claim> PrepareClaims(UserDto userDto)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, userDto.Email));

            foreach (var item in userDto.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            return claims;
        }
    }
}
