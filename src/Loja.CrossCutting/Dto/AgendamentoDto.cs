using Loja.CrossCutting.Enumerators;
using System;

namespace Loja.CrossCutting.Dto
{
    public class AgendamentoDto
    {
        public int AgendamentoId { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataFinalAgendamento { get; set; }
        public string Observacao { get; set; }

        public int ServicoId { get; set; }
        public string UserId { get; set; }
        public int EstabelecimentoId { get; set; }

        //public virtual Servico Servico { get; set; }
        //public virtual User Usuario { get; set; }
        //public virtual Estabelecimento Estabelecimento { get; set; }

        public ESituacao Situacao { get; set; }
    }
}
