using Loja.CrossCutting.Dto;
using Loja.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Application.Validators
{
    public class ServicoDtoValidate : IValidator
    {
        private readonly ServicoDto _servicoDto;
        public List<string> Mensagens { get; }

        public ServicoDtoValidate(ServicoDto servicoDto)
        {
            _servicoDto = servicoDto;
            Mensagens = new List<string>();
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_servicoDto.Nome))
                Mensagens.Add("Informe o nome!");

            if (_servicoDto.Valor == 0)
                Mensagens.Add("Informe o valor!");

            if (_servicoDto.Duracao == 0)
                Mensagens.Add("Informe a duracao!");

            if (_servicoDto.EstabelecimentoId == 0)
                Mensagens.Add("Nenhum estabelecimento vinculado!");

            return !Mensagens.Any();
        }
    }
}
