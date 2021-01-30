using Loja.Domain.Entities;
using System.Threading;

namespace Loja.Test.Builder
{
    public class ServicoBuilder
    {
        private static int nextId;

        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Duracao { get; set; }
        public int SituacaoId { get; set; }
        public int EstabelecimentoId { get; set; }

        public virtual Situacao Situacao { get; set; }
        public virtual Estabelecimento Estabelecimento { get; set; }

        public ServicoBuilder()
        {
            var situacaoBuilder = new SituacaoBuilder().ComSituacaoAtivo().Build();
            var estabelecimentoBuilder = new EstabelecimentoBuilder().ComBarbearia().Build();

            ServicoId = Interlocked.Increment(ref nextId);
            Nome = "Corte";
            Valor = 30;
            Duracao = 60;
            Situacao = situacaoBuilder;
            SituacaoId = situacaoBuilder.SituacaoId;
            Estabelecimento = estabelecimentoBuilder;
            EstabelecimentoId = estabelecimentoBuilder.EstabelecimentoId;

        }

        public Servico Build() => new Servico()
        {
            ServicoId = ServicoId,
            Nome = Nome,
            Valor = Valor,
            Duracao = Duracao,
            SituacaoId = SituacaoId,            
            EstabelecimentoId = EstabelecimentoId,
            Situacao = Situacao,
            Estabelecimento = Estabelecimento
        };

        public ServicoBuilder ComID(int ID)
        {
            ServicoId = ID;
            return this;
        }

        public ServicoBuilder ComCorte()
        {
            ServicoId = 1;
            Nome = "Corte";
            Valor = 30;
            Duracao = 60;
            return this;
        }

        public ServicoBuilder ComSituacaoId(int situacaoId)
        {
            SituacaoId = situacaoId;
            return this;
        }        
    }
}
