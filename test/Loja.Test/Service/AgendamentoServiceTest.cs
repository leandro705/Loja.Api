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
    public class AgendamentoServiceTest
    {
        private IAgendamentoService _agendamentoService { get; set; }
        
        public IMapper _mapper { get; private set; }
        public Mock<IAgendamentoRepository> _agendamentoRepository { get; private set; }
        public Mock<IServicoRepository> _servicoRepository { get; private set; }        


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
            _agendamentoRepository = new Mock<IAgendamentoRepository>();
            _servicoRepository = new Mock<IServicoRepository>();

            _agendamentoService = new AgendamentoService(_agendamentoRepository.Object, _servicoRepository.Object, _mapper);
        }

        [Test]
        public void TestaObterPorIdComAgendamentoNaoCadastrado()
        {
            int agendamentoId = 1;
            Agendamento resultAgendamento = null;
            _agendamentoRepository.Setup(x => x.ObterPorId(agendamentoId)).ReturnsAsync(resultAgendamento);

            var retorno = _agendamentoService.ObterPorId(agendamentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Agendamento não encontrado na base de dados!"));
            Assert.IsTrue(retorno.Result.Errors.Count == 1);
        }

        [Test]
        public void TestaObterPorIdValido()
        {
            int agendamentoId = 1;
            Agendamento resultAgendamento = new AgendamentoBuilder().ComID(agendamentoId).Build();
            _agendamentoRepository.Setup(x => x.ObterPorId(agendamentoId)).ReturnsAsync(resultAgendamento);

            var retorno = _agendamentoService.ObterPorId(agendamentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
            
        }

        [Test]
        public void TestaCreateComInformacoesObrigatoriasNulas()
        {            
            var agendamentoDto = new AgendamentoDto();            
            var retorno = _agendamentoService.Create(agendamentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe a data agendamento!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe a data final agendamento!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Informe o serviço!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Nenhum usuário vinculado!"));
            Assert.IsTrue(retorno.Result.Errors.Contains("Nenhum estabelecimento vinculado!"));

            Assert.IsTrue(retorno.Result.Errors.Count == 5);
        }

        [Test]
        public void TestaCreateComAgendamentoDuplicado()
        {
            var agendamentoId = 1;
            var agendamentoDto = new AgendamentoDtoBuilder().ComID(agendamentoId).Build();
            _agendamentoRepository.Setup(x => x.ValidaAgendamentoDuplicados(agendamentoDto.EstabelecimentoId, agendamentoId, agendamentoDto.DataAgendamento, agendamentoDto.DataFinalAgendamento)).ReturnsAsync(true);

            var retorno = _agendamentoService.Create(agendamentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.ERRO_VALIDACAO);
            Assert.IsTrue(retorno.Result.Errors.Contains("Horário já possui agendamento!"));
            Assert.IsTrue(retorno.Result.Errors.Count == 1);
        }

        [Test]
        public void TestaCreateValido()
        {            
            var agendamentoDto = new AgendamentoDtoBuilder().Build();
            _agendamentoRepository.Setup(x => x.ValidaAgendamentoDuplicados(agendamentoDto.EstabelecimentoId, agendamentoDto.AgendamentoId, agendamentoDto.DataAgendamento, agendamentoDto.DataFinalAgendamento)).ReturnsAsync(false);

            var retorno = _agendamentoService.Create(agendamentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);            
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
            Assert.IsTrue(retorno.Result.Data.SituacaoId == (int)ESituacao.ATIVO);
        }

        [Test]
        public void TestaUpdateValido()
        {
            var agendamentoId = 1;
            var agendamentoDto = new AgendamentoDtoBuilder().ComID(agendamentoId).Build();
            var agendamento = new AgendamentoBuilder().ComID(agendamentoId).Build();
            _agendamentoRepository.Setup(x => x.ObterPorId(agendamentoId)).ReturnsAsync(agendamento);

            var retorno = _agendamentoService.Update(agendamentoDto);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsNotNull(retorno.Result.Data);
        }


        [Test]
        public void TestaDeleteValido()
        {
            var agendamentoId = 1;

            Agendamento resultAgendamento = new AgendamentoBuilder().ComID(agendamentoId).Build();
            _agendamentoRepository.Setup(x => x.ObterPorId(agendamentoId)).ReturnsAsync(resultAgendamento);

            var retorno = _agendamentoService.Delete(agendamentoId);

            Assert.IsTrue(retorno.Result.StatusCode == (int)EStatusCode.OK);
            Assert.IsTrue(retorno.Result.Errors.Count == 0);
            Assert.IsTrue(retorno.Result.Data);
        }
    }
}
