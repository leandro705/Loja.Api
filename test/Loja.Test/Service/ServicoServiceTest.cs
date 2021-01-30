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
    public class ServicoServiceTest
    {
        private IServicoService _servicoService { get; set; }
        
        public IMapper _mapper { get; private set; }        
        public Mock<IServicoRepository> _servicoRepository { get; private set; }        


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();            
            _servicoRepository = new Mock<IServicoRepository>();

            _servicoService = new ServicoService(_servicoRepository.Object, _mapper);
        }

        [Test]
        public void TestaObterPorIdComServicoNaoCadastrado()
        {
            int servicoId = 1;
            Servico resultServico = null;
            _servicoRepository.Setup(x => x.ObterPorId(servicoId)).ReturnsAsync(resultServico);

            var retorno = _servicoService.ObterPorId(servicoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Serviço não encontrado na base de dados!"));
            Assert.IsTrue(retorno.Result.Errors.Count == 1);
        }

        [Test]
        public void TestaObterPorIdValido()
        {
            int servicoId = 1;
            Servico resultServico = new ServicoBuilder().ComID(servicoId).Build();
            _servicoRepository.Setup(x => x.ObterPorId(servicoId)).ReturnsAsync(resultServico);

            var retorno = _servicoService.ObterPorId(servicoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
            
        }

        [Test]
        public void TestaCreateComInformacoesObrigatoriasNulas()
        {            
            var servicoDto = new ServicoDto();            
            var retorno = _servicoService.Create(servicoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe o nome!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe o valor!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe a duracao!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Nenhum estabelecimento vinculado!"));

            Assert.IsTrue(retorno.Result.Errors.Count == 4);
        }
     

        [Test]
        public void TestaCreateValido()
        {            
            var servicoDto = new ServicoDtoBuilder().Build();
            
            var retorno = _servicoService.Create(servicoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);            
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
            Assert.IsTrue(retorno.Result.Data.SituacaoId == (int)ESituacao.ATIVO);
        }

        [Test]
        public void TestaUpdateValido()
        {
            var servicoId = 1;
            var servicoDto = new ServicoDtoBuilder().ComCorte().ComID(servicoId).Build();
            var servico = new ServicoBuilder().ComID(servicoId).Build();
            _servicoRepository.Setup(x => x.ObterPorId(servicoId)).ReturnsAsync(servico);

            var retorno = _servicoService.Update(servicoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
        }


        [Test]
        public void TestaDeleteValido()
        {
            var servicoId = 1;

            var resultServico = new ServicoBuilder().ComID(servicoId).Build();
            _servicoRepository.Setup(x => x.ObterPorId(servicoId)).ReturnsAsync(resultServico);

            var retorno = _servicoService.Delete(servicoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsTrue(retorno.Result.Data);
        }

        [Test]
        public void TestaAtivarValido()
        {
            var servicoId = 1;

            Servico resultServico = new ServicoBuilder().ComID(servicoId).Build();
            _servicoRepository.Setup(x => x.ObterPorId(servicoId)).ReturnsAsync(resultServico);

            var retorno = _servicoService.Ativar(servicoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsTrue(retorno.Result.Data);
        }

        [Test]
        public void TestaDesativarValido()
        {
            var servicoId = 1;

            Servico resultServico = new ServicoBuilder().ComID(servicoId).Build();
            _servicoRepository.Setup(x => x.ObterPorId(servicoId)).ReturnsAsync(resultServico);

            var retorno = _servicoService.Desativar(servicoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsTrue(retorno.Result.Data);
        }
    }
}
