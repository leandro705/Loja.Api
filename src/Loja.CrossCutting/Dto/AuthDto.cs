namespace Loja.CrossCutting.Dto
{
    public class AuthDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public int EstabelecimentoId { get; set; }

    }
}
