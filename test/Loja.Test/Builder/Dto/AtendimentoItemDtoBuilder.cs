using Loja.CrossCutting.Dto;
using System.Threading;

namespace Loja.Test.Builder.Dto
{
    public class AtendimentoItemDtoBuilder
    {
        private static int nextId;

        public int AtendimentoItemId { get; set; }
        public decimal Valor { get; set; }
        public int ServicoId { get; set; }
        public string ServicoNome { get; set; }

        public AtendimentoItemDtoBuilder()
        {
            var servicoBuilder = new ServicoBuilder().ComCorte().Build();

            AtendimentoItemId = Interlocked.Increment(ref nextId);            
            Valor = 30;           
            ServicoId = servicoBuilder.ServicoId;
            ServicoNome = servicoBuilder.Nome;
        }

        public AtendimentoItemDto Build() => new AtendimentoItemDto()
        {
            AtendimentoItemId = AtendimentoItemId,            
            Valor = Valor,
            ServicoId = ServicoId,
            ServicoNome = ServicoNome
        };

        public AtendimentoItemDtoBuilder ComID(int ID)
        {
            AtendimentoItemId = ID;
            return this;
        }        
    }
}
