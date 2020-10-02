namespace Loja.Domain.Entities
{
    public class UserEstabelecimento
    {
        public int EstabelecimentoId { get; set; }    
        public string UserId { get; set; }

        public virtual Estabelecimento Estabelecimento { get; set; }
        public virtual User User { get; set; }

    }
}
