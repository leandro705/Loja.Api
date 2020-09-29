using Loja.CrossCutting.Dto;
using Microsoft.AspNetCore.Identity;
using System;

namespace Loja.Domain.Entities
{
    public class User : IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public Endereco Endereco { get; set; }

        public void AtualizarUsuario(UserDto userDto)
        {
            Nome = userDto.Nome;
            Telefone = userDto.Telefone;
            Celular = userDto.Celular;

            if (Endereco != null)
                Endereco.AtualizarEndereco(userDto.Endereco);
            else
                Endereco = new Endereco(userDto.Endereco);
        }
    }
}
