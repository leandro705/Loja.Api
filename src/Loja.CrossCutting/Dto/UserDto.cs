﻿using System.Collections.Generic;
using System.Security.Claims;

namespace Loja.CrossCutting.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string Role { get; set; }
        public IEnumerable<Claim> Claims { get; set; }        
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
    }
}
