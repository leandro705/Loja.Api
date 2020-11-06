using Loja.Application.Services;
using Loja.CrossCutting.Dto;
using Loja.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Api.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [Route("api/home")]
    public class HomeController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly UserService _userService;
        private readonly IAtendimentoService _atendimentoService;

        public HomeController(IAgendamentoService agendamentoService, IAtendimentoService atendimentoService, UserService userService)
        {
            _agendamentoService = agendamentoService;
            _atendimentoService = atendimentoService;
            _userService = userService;
        }

        [HttpGet("totalAgendamentos")]
        [ProducesResponseType(typeof(ResultDto<int>), 200)]
        public async Task<ResultDto<int>> TotalAgendamentos(int? estabelecimentoId, string usuarioId)
        {
            return await _agendamentoService.TotalAgendamentos(estabelecimentoId, usuarioId);
        }

        [HttpGet("totalUsuarios")]
        [ProducesResponseType(typeof(ResultDto<int>), 200)]
        public async Task<ResultDto<int>> GetTotalCadastrado(int? estabelecimentoId, string usuarioId)
        {
            return await _userService.GetTotalCadastrado(estabelecimentoId, usuarioId);
        }

        [HttpGet("totalAtendimentos")]
        [ProducesResponseType(typeof(ResultDto<int>), 200)]
        public async Task<ResultDto<int>> TotalAtendimentos(int? estabelecimentoId, string usuarioId, int? situacaoId)
        {
            return await _atendimentoService.TotalAtendimentos(estabelecimentoId, usuarioId, situacaoId);
        }

        [HttpGet("totalAtendimentosMes")]
        [ProducesResponseType(typeof(ResultDto<List<TotalizadorMesDto>>), 200)]
        public async Task<ResultDto<List<TotalizadorMesDto>>> TotalAtendimentosMes(int? estabelecimentoId, string usuarioId, int? situacaoId)
        {
            return await _atendimentoService.TotalAtendimentosMes(estabelecimentoId, usuarioId, situacaoId);
        }

        [HttpGet("valorTotal")]
        [ProducesResponseType(typeof(ResultDto<decimal>), 200)]
        public async Task<ResultDto<decimal>> ValorTotal(int? estabelecimentoId, string usuarioId, int? situacaoId)
        {
            return await _atendimentoService.ValorTotal(estabelecimentoId, usuarioId, situacaoId);
        }
    }
}
