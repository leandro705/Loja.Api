using Loja.CrossCutting.Enumerators;
using Loja.CrossCutting.Util;
using Loja.Domain.Entities;
using System.Threading;

namespace Loja.Test.Builder
{
    public class SituacaoBuilder
    {
        private static int nextId;

        public int SituacaoId { get; set; }
        public string Nome { get; set; }
        
        public SituacaoBuilder()
        {
            SituacaoId = Interlocked.Increment(ref nextId);
            Nome = "Ativo";    
        }

        public Situacao Build() => new Situacao()
        {
            SituacaoId = SituacaoId,
            Nome = Nome
        };

        public SituacaoBuilder ComID(int ID)
        {
            SituacaoId = ID;
            return this;
        }

        public SituacaoBuilder ComSituacaoAtivo()
        {
            SituacaoId = (int)ESituacao.ATIVO;
            Nome = ESituacao.ATIVO.GetDescription();
            return this;
        }
    }
}
