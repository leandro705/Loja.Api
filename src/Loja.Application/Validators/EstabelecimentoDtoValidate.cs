using Loja.CrossCutting.Dto;
using Loja.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Application.Validators
{
    public class EstabelecimentoDtoValidate : IValidator
    {
        private readonly EstabelecimentoDto _estabelecimentoDto;
        public List<string> Mensagens { get; }

        public EstabelecimentoDtoValidate(EstabelecimentoDto estabelecimentoDto)
        {
            _estabelecimentoDto = estabelecimentoDto;
            Mensagens = new List<string>();
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_estabelecimentoDto.Nome))
                Mensagens.Add("Informe o nome do estabelecimento!");

            if (string.IsNullOrWhiteSpace(_estabelecimentoDto.Email))
                Mensagens.Add("Informe o e-mail do estabelecimento!");

            if (string.IsNullOrWhiteSpace(_estabelecimentoDto.Url))
                Mensagens.Add("Informe a url do estabelecimento!");

            if (string.IsNullOrWhiteSpace(_estabelecimentoDto.Celular))
                Mensagens.Add("Informe o celular do estabelecimento!");

            if (string.IsNullOrWhiteSpace(_estabelecimentoDto.Descricao))
                Mensagens.Add("Informe a descrição do estabelecimento!");

            return !Mensagens.Any();
        }
    }
}
