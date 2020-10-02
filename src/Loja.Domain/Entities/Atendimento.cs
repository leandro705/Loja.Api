using Loja.CrossCutting.Enumerators;
using System;
using System.Collections.Generic;

namespace Loja.Domain.Entities
{
    public class Atendimento
    {
        public int AtendimentoId { get; set; }        
        public DateTime DataAtendimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }      
        public string UserId { get; set; }
        public int EstabelecimentoId { get; set; }
        public int SituacaoId { get; set; }
        public virtual Situacao Situacao { get; set; }
        public virtual User Usuario { get; set; }
        public virtual Estabelecimento Estabelecimento { get; set; }       
        public virtual IEnumerable<AtendimentoItem> AtendimentoItens { get; set; }
       

    }
}
