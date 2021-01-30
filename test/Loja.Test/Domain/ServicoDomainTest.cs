using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.Test.Builder;
using NUnit.Framework;

namespace Loja.Test.Domain
{
    public class ServicoDomainTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestaDesativacaoServico()
        {
            var resultServico = new ServicoBuilder().Build();

            resultServico.DesabilitarServico();

            Assert.IsTrue(resultServico.SituacaoId == (int)ESituacao.INATIVO);            
        }

        [Test]
        public void TestaCancelamentoServico()
        {
            var resultServico = new ServicoBuilder().Build();

            resultServico.CancelarServico();

            Assert.IsTrue(resultServico.SituacaoId == (int)ESituacao.CANCELADO);
        }

        [Test]
        public void TestaAtivacaoServico()
        {
            var resultServico = new ServicoBuilder().ComSituacaoId((int)ESituacao.CANCELADO).Build();

            resultServico.AtivarServico();

            Assert.IsTrue(resultServico.SituacaoId == (int)ESituacao.ATIVO);
        }

        [Test]
        public void TestaAtualizacaoServico()
        {
            var resultServico = new ServicoBuilder().Build();
            var novoNome = "Novo Serviço";
            var novoValor = 10;
            var novaDuracao = 15;
            var novoEstabelecimento = 2;

            resultServico.AtualizarServico(new ServicoDto{ 
                Nome = novoNome,
                Valor = novoValor,
                Duracao = novaDuracao,
                EstabelecimentoId = novoEstabelecimento

            });

            Assert.IsTrue(resultServico.Nome == novoNome);
            Assert.IsTrue(resultServico.Valor == novoValor);
            Assert.IsTrue(resultServico.Duracao == novaDuracao);
            Assert.IsTrue(resultServico.EstabelecimentoId == novoEstabelecimento);
        }


        
    }
}
