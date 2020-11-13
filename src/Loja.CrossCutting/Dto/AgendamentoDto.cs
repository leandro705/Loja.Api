using System;

namespace Loja.CrossCutting.Dto
{
    public class AgendamentoDto
    {
        public int AgendamentoId { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataFinalAgendamento { get; set; }
        public string DataAgendamentoStr { get; set; }
        public string DataFinalAgendamentoStr { get; set; }
        public string Observacao { get; set; }

        public int ServicoId { get; set; }
        public string ServicoNome { get; set; }
        public decimal ServicoValor { get; set; }
        public string UserId { get; set; }
        public string UsuarioNome { get; set; }

        public string DataCadastro { get; set; }
        public string Situacao { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNome { get; set; }

        public bool PossuiAtendimento { get; set; }
    }
}
