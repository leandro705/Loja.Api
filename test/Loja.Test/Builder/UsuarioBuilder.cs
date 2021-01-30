using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Loja.Test.Builder
{
    public class UsuarioBuilder
    {                
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public int? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual List<UserEstabelecimento> UserEstabelecimentos { get; set; } = new List<UserEstabelecimento>();
        public virtual IEnumerable<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

        public UsuarioBuilder()
        {
            Id = new Guid().ToString();
            Nome = "usuario";
            Email = "usuario@teste.com";                     
        }

        public User Build() => new User(new UserDto()
        {
            Id = Id,
            Nome = Nome,
            Email = Email
        });

        public UsuarioBuilder ComID(string ID)
        {
            Id = ID;
            return this;
        }

        public UsuarioBuilder ComCliente1()
        {
            Id = "e36ab3e7-c6a3-4727-b793-3c5aba3baa29";
            Nome = "Cliente 1";
            Email = "cliente@teste.com";
            return this;
        }
    }
}
