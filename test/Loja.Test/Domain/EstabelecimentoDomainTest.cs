using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.Test.Builder;
using NUnit.Framework;
using System;

namespace Loja.Test.Domain
{
    public class EstabelecimentoDomainTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestaCancelamentoEstabelecimento()
        {
            var resultEstabelecimento = new EstabelecimentoBuilder().Build();

            resultEstabelecimento.CancelarEstabelecimento();

            Assert.IsTrue(resultEstabelecimento.SituacaoId == (int)ESituacao.CANCELADO);
        }   

        [Test]
        public void TestaAtualizacaoAtendimento()
        {
            var resultEstabelecimento = new EstabelecimentoBuilder().Build();

            var nome = "novo nome";
            var email = "novo email";
            var descricao = "nova descricao";
            var url = "nova url";
            var telefone = "67546584565";
            var celular = "67546584565";
            var instagram = "novo instagram";
            var facebook = "nova facebook";            

            resultEstabelecimento.AtualizarEstabelecimento(new EstabelecimentoDto
            {
                Nome = nome,
                Email = email,
                Descricao = descricao,
                Url = url,
                Telefone = telefone,
                Celular = celular,
                Instagram = instagram,
                Facebook = facebook

            });
            
            Assert.IsTrue(resultEstabelecimento.Nome == nome);
            Assert.IsTrue(resultEstabelecimento.Email == email);
            Assert.IsTrue(resultEstabelecimento.Descricao == descricao);
            Assert.IsTrue(resultEstabelecimento.Url == url);
            Assert.IsTrue(resultEstabelecimento.Telefone == telefone);
            Assert.IsTrue(resultEstabelecimento.Celular == celular);
            Assert.IsTrue(resultEstabelecimento.Instagram == instagram);
            Assert.IsTrue(resultEstabelecimento.Facebook == facebook);
        }        
    }
}
