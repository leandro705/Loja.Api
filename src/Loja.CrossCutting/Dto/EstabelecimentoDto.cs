using Loja.CrossCutting.Enumerators;

namespace Loja.CrossCutting.Dto
{
    public class EstabelecimentoDto
    {
        public int EstabelecimentoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string DataCadastro { get; set; }
        public string Situacao { get; set; }
    }
}
