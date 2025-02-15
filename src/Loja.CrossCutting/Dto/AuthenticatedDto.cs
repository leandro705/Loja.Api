﻿namespace Loja.CrossCutting.Dto
{
    public class AuthenticatedDto
    {
        public string Id { get; set; }
        public int EstabelecimentoId { get; set; }        
        public string EstabelecimentoNomeUrl { get; set; }
        public string EstabelecimentoNome { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public bool IsAdministrador { get; set; }
        
    }
}
