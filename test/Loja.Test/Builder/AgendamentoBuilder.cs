using Loja.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Loja.Test.Builder
{
    public class AgendamentoBuilder
    {
        private static int nextId;

        public int AgendamentoId { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataFinalAgendamento { get; set; }
        public string Observacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public int ServicoId { get; set; }
        public string UserId { get; set; }
        public int EstabelecimentoId { get; set; }

        public Servico Servico { get; set; }
        public User Usuario { get; set; }
        public Estabelecimento Estabelecimento { get; set; }

        public int SituacaoId { get; set; }
        public Situacao Situacao { get; set; }

        public List<Atendimento> Atendimentos { get; set; }

        public AgendamentoBuilder()
        {
            var situacaoBuilder = new SituacaoBuilder().ComSituacaoAtivo().Build();
            var servicoBuilder = new ServicoBuilder().ComCorte().Build();
            var estabelecimentoBuilder = new EstabelecimentoBuilder().ComBarbearia().Build();
            var usuarioBuilder = new UsuarioBuilder().ComCliente1().Build();

            AgendamentoId = Interlocked.Increment(ref nextId);
            DataAgendamento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
            DataFinalAgendamento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0);

            Situacao = situacaoBuilder;
            SituacaoId = situacaoBuilder.SituacaoId;
            Estabelecimento = estabelecimentoBuilder;
            EstabelecimentoId = estabelecimentoBuilder.EstabelecimentoId;
            Usuario = usuarioBuilder;
            UserId = usuarioBuilder.Id;
            Servico = servicoBuilder;
            ServicoId = servicoBuilder.ServicoId;
            Atendimentos = new List<Atendimento>();
        }

        public Agendamento Build() => new Agendamento()
        {
            AgendamentoId = AgendamentoId,
            DataAgendamento = DataAgendamento,
            DataFinalAgendamento = DataFinalAgendamento,
            Observacao = Observacao,
            DataCadastro = DataCadastro,
            EstabelecimentoId = EstabelecimentoId,
            Estabelecimento = Estabelecimento,
            ServicoId = ServicoId,
            Servico = Servico,
            UserId = UserId,
            Usuario = Usuario,
            SituacaoId = SituacaoId,
            Situacao = Situacao,
            Atendimentos = Atendimentos
        };

        public AgendamentoBuilder ComID(int ID)
        {
            AgendamentoId = ID;
            return this;
        }

        public AgendamentoBuilder ComDataAgendamento(DateTime dataAgendamento)
        {
            DataAgendamento = dataAgendamento;
            return this;
        }

        public AgendamentoBuilder ComDataFinalAgendamento(DateTime dataFinalAgendamento)
        {
            DataFinalAgendamento = dataFinalAgendamento;
            return this;
        }


    }
}
