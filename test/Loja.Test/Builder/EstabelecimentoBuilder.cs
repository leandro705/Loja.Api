using Loja.Domain.Entities;
using System.Threading;

namespace Loja.Test.Builder
{
    public class EstabelecimentoBuilder
    {
        private static int nextId;

        public int EstabelecimentoId { get; set; }
        public string Nome { get; set; }
        public int SituacaoId { get; set; }
        public virtual Situacao Situacao { get; set; }
        
        public EstabelecimentoBuilder()
        {
            var situacaoBuilder = new SituacaoBuilder().ComSituacaoAtivo().Build();

            EstabelecimentoId = Interlocked.Increment(ref nextId);
            Nome = "Loja 1";
            Situacao = situacaoBuilder;
            SituacaoId = situacaoBuilder.SituacaoId;
        }

        public Estabelecimento Build() => new Estabelecimento()
        {
            EstabelecimentoId = EstabelecimentoId,
            Nome = Nome,          
            SituacaoId = SituacaoId,
            Situacao = Situacao
        };

        public EstabelecimentoBuilder ComID(int ID)
        {
            EstabelecimentoId = ID;
            return this;
        }

        public EstabelecimentoBuilder ComBarbearia()
        {
            EstabelecimentoId = 1;
            Nome = "Barbearia";
            return this;
        }
    }
}
