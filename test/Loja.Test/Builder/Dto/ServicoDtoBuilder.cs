using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.CrossCutting.Util;
using System.Threading;

namespace Loja.Test.Builder.Dto
{
    public class ServicoDtoBuilder
    {
        private static int nextId;

        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Duracao { get; set; }
        public string Situacao { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNome { get; set; }

        public ServicoDtoBuilder()
        {            
            var estabelecimentoBuilder = new EstabelecimentoDtoBuilder().ComBarbearia().Build();

            ServicoId = Interlocked.Increment(ref nextId);
            Nome = "Corte";
            Valor = 30;
            Duracao = 60;
            Situacao = ESituacao.ATIVO.GetDescription();
            
            EstabelecimentoId = estabelecimentoBuilder.EstabelecimentoId;
            EstabelecimentoNome = estabelecimentoBuilder.Nome;
        }

        public ServicoDto Build() => new ServicoDto()
        {
            ServicoId = ServicoId,
            Nome = Nome,
            Valor = Valor,
            Duracao = Duracao,
            Situacao = Situacao,
            EstabelecimentoId = EstabelecimentoId,            
            EstabelecimentoNome = EstabelecimentoNome
        };

        public ServicoDtoBuilder ComID(int ID)
        {
            ServicoId = ID;
            return this;
        }

        public ServicoDtoBuilder ComCorte()
        {
            ServicoId = 1;
            Nome = "Corte";
            Valor = 30;
            Duracao = 60;
            return this;
        }
    }
}
