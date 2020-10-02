using Loja.CrossCutting.Enumerators;
using System;

namespace Loja.CrossCutting.Dto
{
    public class AtendimentoDto
    {
        public int AtendimentoId { get; set; }
        public DateTime DataAtendimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public string UserId { get; set; }
        public int EstabelecimentoId { get; set; }
        public ESituacao Situacao { get; set; }

        //public virtual User Usuario { get; set; }
        //public virtual Estabelecimento Estabelecimento { get; set; }
        //public virtual IEnumerable<AtendimentoItem> AtendimentoItens { get; set; }
    }
}
