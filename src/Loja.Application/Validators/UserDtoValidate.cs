using Loja.CrossCutting.Dto;
using Loja.Domain.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Application.Validators
{
    public class UserDtoValidate : IValidator
    {
        private readonly UserDto _userDto;
        public List<string> Mensagens { get; }

        public UserDtoValidate(UserDto userDto)
        {
            _userDto = userDto;
            Mensagens = new List<string>();
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_userDto.Nome))
                Mensagens.Add("Informe o nome!");

            if (string.IsNullOrWhiteSpace(_userDto.Email))
                Mensagens.Add("Informe o e-mail!");

            if (string.IsNullOrWhiteSpace(_userDto.Senha))
                Mensagens.Add("Informe a senha!");

            return !Mensagens.Any();
        }
    }
}
