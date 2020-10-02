namespace Loja.Domain.Entities
{
    public class AtendimentoItem
    {
        public int AtendimentoItemId { get; set; }
        public decimal Valor { get; set; }
        public int ServicoId { get; set; }        

        public virtual Servico Servico { get; set; }
    }
}
