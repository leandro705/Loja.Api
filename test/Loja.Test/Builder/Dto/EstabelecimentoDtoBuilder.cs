using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.CrossCutting.Util;
using System.Threading;

namespace Loja.Test.Builder.Dto
{
    public class EstabelecimentoDtoBuilder
    {
        private static int nextId;

        public int EstabelecimentoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Situacao { get; set; }

        public EstabelecimentoDtoBuilder()
        {           
            EstabelecimentoId = Interlocked.Increment(ref nextId);
            Nome = "Loja 1";
            Email = "Loja@teste.com";
            Descricao = "Descrição loja";
            Url = "url loja";
            Celular = "Celular Loja";            
            Situacao = ESituacao.ATIVO.GetDescription();
        }

        public EstabelecimentoDto Build() => new EstabelecimentoDto()
        {
            EstabelecimentoId = EstabelecimentoId,
            Nome = Nome,
            Email = Email,
            Descricao = Descricao,
            Url = Url,
            Celular = Celular,
            Situacao = Situacao
        };

        public EstabelecimentoDtoBuilder ComID(int ID)
        {
            EstabelecimentoId = ID;
            return this;
        }

        public EstabelecimentoDtoBuilder ComBarbearia()
        {
            EstabelecimentoId = 1;
            Nome = "Barbearia";
            return this;
        }
    }
}
