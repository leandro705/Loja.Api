namespace Loja.CrossCutting.Dto
{
    public class AtendimentoItemDto
    {
        public int AtendimentoItemId { get; set; }
        public decimal Valor { get; set; }
        public string ValorFormatado { get; set; }
        public int ServicoId { get; set; }
        public string ServicoNome { get; set; }
    }
}
