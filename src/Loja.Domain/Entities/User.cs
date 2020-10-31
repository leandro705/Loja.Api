using Loja.CrossCutting.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Loja.Domain.Entities
{
    public class User : IdentityUser
    {
        protected User() { }
        public User(UserDto userDto)
        {
            Nome = userDto.Nome;
            UserName = userDto.Email;
            Email = userDto.Email;
            Celular = userDto.Celular;
            IsFacebook = userDto.IsFacebook;
            IsGoogle = userDto.IsGoogle;
            DataCadastro = DateTime.Now;
            Endereco = new Endereco(userDto.Endereco);
        }

        public override string Id { get => base.Id; set => base.Id = value; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public int? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual IEnumerable<UserEstabelecimento> UserEstabelecimentos { get; set; } = new List<UserEstabelecimento>();
        public virtual IEnumerable<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

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

        public void VincularEstabelecimento(List<UserEstabelecimento> userEstabelecimentos)
        {
            UserEstabelecimentos = userEstabelecimentos;
        }
    }
}
