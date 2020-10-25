using Loja.CrossCutting.Dto;
using Loja.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Application.Validators
{
    public class AgendamentoDtoValidate : IValidator
    {
        private readonly AgendamentoDto _agendamentoDto;
        public List<string> Mensagens { get; }

        public AgendamentoDtoValidate(AgendamentoDto agendamentoDto)
        {
            _agendamentoDto = agendamentoDto;
            Mensagens = new List<string>();
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_agendamentoDto.DataAgendamentoStr))
                Mensagens.Add("Informe a data agendamento!");

            if (string.IsNullOrWhiteSpace(_agendamentoDto.DataFinalAgendamentoStr))
                Mensagens.Add("Informe a data final agendamento!");

            if (_agendamentoDto.ServicoId == 0)
                Mensagens.Add("Informe o serviço!");

            if (string.IsNullOrWhiteSpace(_agendamentoDto.UserId))
                Mensagens.Add("Nenhum usuário vinculado!");

            if (_agendamentoDto.EstabelecimentoId == 0)
                Mensagens.Add("Nenhum estabelecimento vinculado!");

            return !Mensagens.Any();
        }
    }
}
