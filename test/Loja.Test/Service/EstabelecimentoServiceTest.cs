using AutoMapper;
using Loja.Application.Mapper;
using Loja.Application.Services;
using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using Loja.Test.Builder;
using Loja.Test.Builder.Dto;
using Moq;
using NUnit.Framework;

namespace Loja.Test.Service
{
    public class EstabelecimentoServiceTest
    {
        private IEstabelecimentoService _estabelecimentoService { get; set; }
        
        public IMapper _mapper { get; private set; }        
        public Mock<IEstabelecimentoRepository> _estabelecimentoRepository { get; private set; }        


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
            _estabelecimentoRepository = new Mock<IEstabelecimentoRepository>();

            _estabelecimentoService = new EstabelecimentoService(_estabelecimentoRepository.Object, _mapper);
        }

        [Test]
        public void TestaObterPorIdComEstabelecimentoNaoCadastrado()
        {
            int estabelecimentoId = 1;
            Estabelecimento resultEstabelecimento = null;
            _estabelecimentoRepository.Setup(x => x.ObterPorId(estabelecimentoId)).ReturnsAsync(resultEstabelecimento);

            var retorno = _estabelecimentoService.ObterPorId(estabelecimentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Estabelecimento não encontrado na base de dados!"));
            Assert.IsTrue(retorno.Result.Errors.Count == 1);
        }

        [Test]
        public void TestaObterPorIdValido()
        {
            int estabelecimentoId = 1;
            Estabelecimento resultEstabelecimento = new EstabelecimentoBuilder().ComID(estabelecimentoId).Build();
            _estabelecimentoRepository.Setup(x => x.ObterPorId(estabelecimentoId)).ReturnsAsync(resultEstabelecimento);

            var retorno = _estabelecimentoService.ObterPorId(estabelecimentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
            
        }

        [Test]
        public void TestaCreateComInformacoesObrigatoriasNulas()
        {            
            var estabelecimentoDto = new EstabelecimentoDto();            
            var retorno = _estabelecimentoService.Create(estabelecimentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe o nome do estabelecimento!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe o e-mail do estabelecimento!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe a url do estabelecimento!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe o celular do estabelecimento!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe a descrição do estabelecimento!"));

            Assert.IsTrue(retorno.Result.Errors.Count == 5);
        }
     

        [Test]
        public void TestaCreateValido()
        {            
            var estabelecimentoDto = new EstabelecimentoDtoBuilder().Build();
            
            var retorno = _estabelecimentoService.Create(estabelecimentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);            
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
            Assert.IsTrue(retorno.Result.Data.SituacaoId == (int)ESituacao.ATIVO);
        }

        [Test]
        public void TestaUpdateValido()
        {
            var estabelecimentoId = 1;
            var estabelecimentoDto = new EstabelecimentoDtoBuilder().ComBarbearia().ComID(estabelecimentoId).Build();
            var estabelecimento = new EstabelecimentoBuilder().ComID(estabelecimentoId).Build();
            _estabelecimentoRepository.Setup(x => x.ObterPorId(estabelecimentoId)).ReturnsAsync(estabelecimento);

            var retorno = _estabelecimentoService.Update(estabelecimentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);            
        }


        [Test]
        public void TestaDeleteValido()
        {
            var estabelecimentoId = 1;

            var resultEstabelecimento = new EstabelecimentoBuilder().ComID(estabelecimentoId).Build();
            _estabelecimentoRepository.Setup(x => x.ObterPorId(estabelecimentoId)).ReturnsAsync(resultEstabelecimento);

            var retorno = _estabelecimentoService.Delete(estabelecimentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsTrue(retorno.Result.Data);
        }      
    }
}
