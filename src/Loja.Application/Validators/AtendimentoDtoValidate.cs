using Loja.CrossCutting.Dto;
using Loja.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Application.Validators
{
    public class AtendimentoDtoValidate : IValidator
    {
        private readonly AtendimentoDto _atendimentoDto;
        public List<string> Mensagens { get; }

        public AtendimentoDtoValidate(AtendimentoDto atendimentoDto)
        {
            _atendimentoDto = atendimentoDto;
            Mensagens = new List<string>();
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_atendimentoDto.DataAtendimento))
                Mensagens.Add("Informe a data atendimento!");           

            if (_atendimentoDto.Valor == 0)
                Mensagens.Add("Informe o valor!");

            if (string.IsNullOrWhiteSpace(_atendimentoDto.UserId))
                Mensagens.Add("Nenhum usuário vinculado!");

            if (_atendimentoDto.EstabelecimentoId == 0)
                Mensagens.Add("Nenhum estabelecimento vinculado!");

            if (!_atendimentoDto.AtendimentoItens.Any())
                Mensagens.Add("Nenhum serviço vinculado!");

            return !Mensagens.Any();
        }
    }
}
