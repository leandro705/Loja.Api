﻿namespace Loja.CrossCutting.Dto
{
    public class AuthDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool IsGoogle { get; set; }
       
    }
}
