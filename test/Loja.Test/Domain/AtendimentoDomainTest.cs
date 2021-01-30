using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.Test.Builder;
using NUnit.Framework;
using System;

namespace Loja.Test.Domain
{
    public class AtendimentoDomainTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestaCancelamentoAtendimento()
        {
            var resultAtendimento = new AtendimentoBuilder().Build();

            resultAtendimento.CancelarAtendimento();

            Assert.IsTrue(resultAtendimento.SituacaoId == (int)ESituacao.CANCELADO);
        }

        [Test]
        public void TestaFinalizacaoAtendimento()
        {
            var resultAtendimento = new AtendimentoBuilder().Build();

            resultAtendimento.FinalizarAtendimento();

            Assert.IsTrue(resultAtendimento.SituacaoId == (int)ESituacao.FINALIZADO);
        }

        [Test]
        public void TestaAtualizacaoAtendimento()
        {
            var resultAtendimento = new AtendimentoBuilder().Build();

            var dataAtendimento = DateTime.Now.AddDays(2);            
            var observacao = "Nova observação";
            var valor = 22;
            var desconto = 2;
            var valorTotal = 20;
            var userId = new Guid().ToString();
            var novoEstabelecimento = 2;

            resultAtendimento.AtualizarAtendimento(new AtendimentoDto
            {
                DataAtendimento = dataAtendimento.ToString("dd/MM/yyyy HH:mm:ss"),             
                Observacao = observacao,
                Valor = valor,
                Desconto = desconto,
                ValorTotal = valorTotal,
                UserId = userId,
                EstabelecimentoId = novoEstabelecimento

            });

            Assert.IsTrue(resultAtendimento.DataAtendimento.ToString() == dataAtendimento.ToString());            
            Assert.IsTrue(resultAtendimento.Observacao == observacao);
            Assert.IsTrue(resultAtendimento.Valor == valor);
            Assert.IsTrue(resultAtendimento.Desconto == desconto);
            Assert.IsTrue(resultAtendimento.ValorTotal == valorTotal);
            Assert.IsTrue(resultAtendimento.UserId == userId);
            Assert.IsTrue(resultAtendimento.EstabelecimentoId == novoEstabelecimento);
        }        
    }
}
