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
    public class AtendimentoServiceTest
    {
        private IAtendimentoService _atendimentoService { get; set; }
        
        public IMapper _mapper { get; private set; }        
        public Mock<IAtendimentoRepository> _atendimentoRepository { get; private set; }
        public Mock<IAgendamentoRepository> _agendamentoRepository { get; private set; }


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
            _atendimentoRepository = new Mock<IAtendimentoRepository>();
            _agendamentoRepository = new Mock<IAgendamentoRepository>();

            _atendimentoService = new AtendimentoService(_atendimentoRepository.Object, _agendamentoRepository.Object, _mapper);
        }

        [Test]
        public void TestaObterPorIdComAtendimentoNaoCadastrado()
        {
            int atendimentoId = 1;
            Atendimento resultAtendimento = null;
            _atendimentoRepository.Setup(x => x.ObterPorId(atendimentoId)).ReturnsAsync(resultAtendimento);

            var retorno = _atendimentoService.ObterPorId(atendimentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Atendimento não encontrado na base de dados!"));
            Assert.IsTrue(retorno.Result.Errors.Count == 1);
        }

        [Test]
        public void TestaObterPorIdValido()
        {
            int atendimentoId = 1;
            Atendimento resultAtendimento = new AtendimentoBuilder().ComID(atendimentoId).Build();
            _atendimentoRepository.Setup(x => x.ObterPorId(atendimentoId)).ReturnsAsync(resultAtendimento);

            var retorno = _atendimentoService.ObterPorId(atendimentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
            
        }

        [Test]
        public void TestaCreateComInformacoesObrigatoriasNulas()
        {            
            var atendimentoDto = new AtendimentoDto();            
            var retorno = _atendimentoService.Create(atendimentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe a data atendimento!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe o valor!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Nenhum usuário vinculado!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Nenhum estabelecimento vinculado!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Nenhum serviço vinculado!"));

            Assert.IsTrue(retorno.Result.Errors.Count == 5);
        }
     

        [Test]
        public void TestaCreateValido()
        {            
            var atendimentoDto = new AtendimentoDtoBuilder().Build();
            
            var retorno = _atendimentoService.Create(atendimentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);            
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
            Assert.IsTrue(retorno.Result.Data.SituacaoId == (int)ESituacao.PENDENTE);
        }

        [Test]
        public void TestaUpdateValido()
        {
            var atendimentoId = 1;
            var atendimentoDto = new AtendimentoDtoBuilder().ComID(atendimentoId).Build();
            var atendimento = new AtendimentoBuilder().ComID(atendimentoId).Build();
            _atendimentoRepository.Setup(x => x.ObterPorId(atendimentoId)).ReturnsAsync(atendimento);

            var retorno = _atendimentoService.Update(atendimentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);            
        }


        [Test]
        public void TestaDeleteValido()
        {
            var atendimentoId = 1;

            var resultAtendimento = new AtendimentoBuilder().ComID(atendimentoId).Build();
            _atendimentoRepository.Setup(x => x.ObterPorId(atendimentoId)).ReturnsAsync(resultAtendimento);

            var retorno = _atendimentoService.Delete(atendimentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsTrue(retorno.Result.Data);
        }      
    }
}
