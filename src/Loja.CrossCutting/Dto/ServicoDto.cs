namespace Loja.CrossCutting.Dto
{
    public class ServicoDto
    {
        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Duracao { get; set; }
        public string ValorFormatado { get; set; }
        public string DataCadastro { get; set; }
        public string Situacao { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNome { get; set; }        
    }
}
