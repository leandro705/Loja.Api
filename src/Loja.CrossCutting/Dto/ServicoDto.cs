namespace Loja.CrossCutting.Dto
{
    public class ServicoDto
    {
        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string DataCadastro { get; set; }
        public string Situacao { get; set; }
    }
}
