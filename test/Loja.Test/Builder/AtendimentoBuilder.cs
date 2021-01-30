using Loja.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Loja.Test.Builder
{
    public class AtendimentoBuilder
    {
        private static int nextId;

        public int AtendimentoId { get; set; }
        public DateTime DataAtendimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }

        public string UserId { get; set; }
        public int EstabelecimentoId { get; set; }
        public int? AgendamentoId { get; set; }

        public int SituacaoId { get; set; }
        public virtual Situacao Situacao { get; set; }
        public virtual User Usuario { get; set; }
        public virtual Estabelecimento Estabelecimento { get; set; }
        public virtual Agendamento Agendamento { get; set; }
        public virtual IEnumerable<AtendimentoItem> AtendimentoItens { get; set; }

        public AtendimentoBuilder()
        {
            var situacaoBuilder = new SituacaoBuilder().ComSituacaoAtivo().Build();            
            var estabelecimentoBuilder = new EstabelecimentoBuilder().ComBarbearia().Build();
            var usuarioBuilder = new UsuarioBuilder().ComCliente1().Build();

            AtendimentoId = Interlocked.Increment(ref nextId);
            DataAtendimento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
            Valor = 30;
            Desconto = 0;
            ValorTotal = 30;
            Situacao = situacaoBuilder;
            SituacaoId = situacaoBuilder.SituacaoId;
            Estabelecimento = estabelecimentoBuilder;
            EstabelecimentoId = estabelecimentoBuilder.EstabelecimentoId;
            Usuario = usuarioBuilder;
            UserId = usuarioBuilder.Id;            
            AtendimentoItens = new List<AtendimentoItem>();
        }

        public Atendimento Build() => new Atendimento()
        {
            AtendimentoId = AtendimentoId,
            DataAtendimento = DataAtendimento,
            Valor = Valor,
            Desconto = Desconto,
            ValorTotal = ValorTotal,
            Observacao = Observacao,
            DataCadastro = DataCadastro,
            EstabelecimentoId = EstabelecimentoId,
            Estabelecimento = Estabelecimento,
            UserId = UserId,
            Usuario = Usuario,
            SituacaoId = SituacaoId,
            Situacao = Situacao,
            AtendimentoItens = AtendimentoItens
        };

        public AtendimentoBuilder ComID(int ID)
        {
            AtendimentoId = ID;
            return this;
        }
    }
}
