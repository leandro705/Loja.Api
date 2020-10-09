using Loja.CrossCutting.Enumerators;
using System;

namespace Loja.CrossCutting.Dto
{
    public class AgendamentoDto
    {
        public int AgendamentoId { get; set; }
        public string DataAgendamento { get; set; }
        public string DataFinalAgendamento { get; set; }
        public string Observacao { get; set; }

        public int ServicoId { get; set; }
        public string ServicoNome { get; set; }
        public string UserId { get; set; }       

        public string DataCadastro { get; set; }
        public string Situacao { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNome { get; set; }
    }
}
