using System.Collections.Generic;
using System.Security.Claims;

namespace Loja.CrossCutting.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string Role { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNomeUrl { get; set; }
        public string EstabelecimentoNome { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public IEnumerable<EstabelecimentoDto> Estabelecimentos { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public EnderecoDto Endereco { get; set; }
        public string DataCadastro { get; set; }


    }
}
