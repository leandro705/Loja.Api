using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.Test.Builder;
using NUnit.Framework;
using System;

namespace Loja.Test.Domain
{
    public class AgendamentoDomainTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestaCancelamentoAgendamento()
        {
            var resultAgendamento = new AgendamentoBuilder().Build();

            resultAgendamento.CancelarAgendamento();

            Assert.IsTrue(resultAgendamento.SituacaoId == (int)ESituacao.CANCELADO);            
        }       

        [Test]
        public void TestaFinalizacaoAgendamento()
        {
            var resultAgendamento = new AgendamentoBuilder().Build();

            resultAgendamento.FinalizarAgendamento();

            Assert.IsTrue(resultAgendamento.SituacaoId == (int)ESituacao.FINALIZADO);
        }

        [Test]
        public void TestaAtualizacaoAgendamento()
        {
            var resultAgendamento = new AgendamentoBuilder().Build();
            var dataAgendamento = DateTime.Now.AddDays(2);
            var dataFinalAgendamento = DateTime.Now.AddDays(2).AddHours(1);
            var observacao = "Nova observação";
            var servicoId = 2;
            var userId = new Guid().ToString();
            var novoEstabelecimento = 2;

            resultAgendamento.AtualizarAgendamento(new AgendamentoDto
            {
                DataAgendamentoStr = dataAgendamento.ToString("dd/MM/yyyy HH:mm:ss"),
                DataFinalAgendamentoStr = dataFinalAgendamento.ToString("dd/MM/yyyy HH:mm:ss"),
                Observacao = observacao,
                ServicoId = servicoId,
                UserId = userId,
                EstabelecimentoId = novoEstabelecimento
            });

            Assert.IsTrue(resultAgendamento.DataAgendamento.ToString() == dataAgendamento.ToString());
            Assert.IsTrue(resultAgendamento.DataFinalAgendamento.ToString() == dataFinalAgendamento.ToString());
            Assert.IsTrue(resultAgendamento.Observacao == observacao);
            Assert.IsTrue(resultAgendamento.ServicoId == servicoId);
            Assert.IsTrue(resultAgendamento.UserId == userId);
            Assert.IsTrue(resultAgendamento.EstabelecimentoId == novoEstabelecimento);
        }

        [TestCase(1, 2, "2021-01-01 10:00", "2021-01-01 11:00")]
        [TestCase(1, 2, "2021-01-01 09:30", "2021-01-01 10:30")]
        [TestCase(1, 2, "2021-01-01 10:20", "2021-01-01 10:50")]
        [TestCase(1, 2, "2021-01-01 10:30", "2021-01-01 11:00")]
        public void TestaAgendamentoAgendamentoDuplicado(int estabelecimentoId, int agendamentoId, string dataAgendamentoStr, string dataFinalAgendamentoStr)
        {
            var dataAgendamento = DateTime.Parse(dataAgendamentoStr);
            var dataFinalAgendamento = DateTime.Parse(dataFinalAgendamentoStr);

            var dataAgendamentoCadastrado = DateTime.Parse("2021-01-01 10:00");
            var dataFinalAgendamentoCadastrado = DateTime.Parse("2021-01-01 11:00");

            var resultAgendamento = new AgendamentoBuilder()
                                    .ComID(1)
                                    .ComDataAgendamento(dataAgendamentoCadastrado)
                                    .ComDataFinalAgendamento(dataFinalAgendamentoCadastrado)
                                    .Build();

            var validacao = resultAgendamento.VerificaAgendamentoDuplicado(estabelecimentoId, agendamentoId, dataAgendamento, dataFinalAgendamento);

            Assert.IsTrue(validacao);
        }

        [TestCase(2, 2, "2021-01-01 10:00", "2021-01-01 11:00")]
        [TestCase(2, 2, "2021-01-01 09:30", "2021-01-01 10:30")]
        [TestCase(2, 2, "2021-01-01 10:20", "2021-01-01 10:50")]
        [TestCase(2, 2, "2021-01-01 10:30", "2021-01-01 11:00")]
        public void TestaAgendamentoAgendamentoDuplicadoComEstabelecimentoDiferentes(int estabelecimentoId, int agendamentoId, string dataAgendamentoStr, string dataFinalAgendamentoStr)
        {
            var dataAgendamento = DateTime.Parse(dataAgendamentoStr);
            var dataFinalAgendamento = DateTime.Parse(dataFinalAgendamentoStr);

            var dataAgendamentoCadastrado = DateTime.Parse("2021-01-01 10:00");
            var dataFinalAgendamentoCadastrado = DateTime.Parse("2021-01-01 11:00");

            var resultAgendamento = new AgendamentoBuilder()
                                    .ComID(1)
                                    .ComDataAgendamento(dataAgendamentoCadastrado)
                                    .ComDataFinalAgendamento(dataFinalAgendamentoCadastrado)
                                    .Build();

            var validacao = resultAgendamento.VerificaAgendamentoDuplicado(estabelecimentoId, agendamentoId, dataAgendamento, dataFinalAgendamento);

            Assert.IsFalse(validacao);
        }

        [TestCase(1, 2, "2021-01-02 10:00", "2021-01-02 11:00")]
        [TestCase(1, 2, "2021-01-01 09:00", "2021-01-01 09:30")]
        [TestCase(1, 2, "2021-01-01 11:30", "2021-01-01 12:00")]
        public void TestaAgendamentoAgendamentoNaoDuplicado(int estabelecimentoId, int agendamentoId, string dataAgendamentoStr, string dataFinalAgendamentoStr)
        {
            var dataAgendamento = DateTime.Parse(dataAgendamentoStr);
            var dataFinalAgendamento = DateTime.Parse(dataFinalAgendamentoStr);

            var dataAgendamentoCadastrado = DateTime.Parse("2021-01-01 10:00");
            var dataFinalAgendamentoCadastrado = DateTime.Parse("2021-01-01 11:00");

            var resultAgendamento = new AgendamentoBuilder()
                                    .ComID(1)
                                    .ComDataAgendamento(dataAgendamentoCadastrado)
                                    .ComDataFinalAgendamento(dataFinalAgendamentoCadastrado)
                                    .Build();

            var validacao = resultAgendamento.VerificaAgendamentoDuplicado(estabelecimentoId, agendamentoId, dataAgendamento, dataFinalAgendamento);

            Assert.IsFalse(validacao);
        }



    }
}
