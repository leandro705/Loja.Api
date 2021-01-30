using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.CrossCutting.Util;
using System;
using System.Threading;

namespace Loja.Test.Builder.Dto
{
    public class AgendamentoDtoBuilder
    {
        private static int nextId;

        public int AgendamentoId { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataFinalAgendamento { get; set; }
        public string DataAgendamentoStr { get; set; }
        public string DataFinalAgendamentoStr { get; set; }
     
        public int ServicoId { get; set; }
        public string ServicoNome { get; set; }
        public decimal ServicoValor { get; set; }
        public string UserId { get; set; }
        public string UsuarioNome { get; set; }

        public string Situacao { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNome { get; set; }

        public AgendamentoDtoBuilder()
        {
            var servicoBuilder = new ServicoBuilder().ComCorte().Build();
            var estabelecimentoDtoBuilder = new EstabelecimentoDtoBuilder().ComBarbearia().Build();
            var usuarioDtoBuilder = new UsuarioDtoBuilder().ComCliente1().Build();

            AgendamentoId = Interlocked.Increment(ref nextId);
            DataAgendamento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
            DataFinalAgendamento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0);
            DataAgendamentoStr = DateTime.Now.Day + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year + " 10:00";
            DataFinalAgendamentoStr = DateTime.Now.Day + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year + " 11:00";

            ServicoId = servicoBuilder.ServicoId;
            ServicoNome = servicoBuilder.Nome;
            ServicoValor = servicoBuilder.Valor;

            UserId = usuarioDtoBuilder.Id;
            UsuarioNome = usuarioDtoBuilder.Nome;

            Situacao = ESituacao.ATIVO.GetDescription();

            EstabelecimentoId = estabelecimentoDtoBuilder.EstabelecimentoId;
            EstabelecimentoNome = estabelecimentoDtoBuilder.Nome;

        }

        public AgendamentoDto Build() => new AgendamentoDto()
        {
            AgendamentoId = AgendamentoId,
            DataAgendamento = DataAgendamento,
            DataFinalAgendamento = DataFinalAgendamento,
            DataAgendamentoStr = DataAgendamentoStr,
            DataFinalAgendamentoStr = DataFinalAgendamentoStr,
            ServicoId = ServicoId,
            ServicoNome = ServicoNome,
            ServicoValor = ServicoValor,
            UserId = UserId,
            UsuarioNome = UsuarioNome,                        
            Situacao = Situacao,
            EstabelecimentoId = EstabelecimentoId,
            EstabelecimentoNome = EstabelecimentoNome            
        };

        public AgendamentoDtoBuilder ComID(int ID)
        {
            AgendamentoId = ID;
            return this;
        }
    }
}
