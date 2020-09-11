using Loja.CrossCutting.Dto;
using Loja.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Application.Validators
{
    public class AuthDtoValidate : IValidator
    {
        private readonly AuthDto _authDto;
        public List<string> Mensagens { get; }

        public AuthDtoValidate(AuthDto authDto)
        {
            _authDto = authDto;
            Mensagens = new List<string>();
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_authDto.Email))
                Mensagens.Add("Informe o e-mail!");

            if (string.IsNullOrWhiteSpace(_authDto.Password))
                Mensagens.Add("Informe a senha!");

            return !Mensagens.Any();
        }
    }
}
