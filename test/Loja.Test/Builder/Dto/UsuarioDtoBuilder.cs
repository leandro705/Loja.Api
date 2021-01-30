using Loja.CrossCutting.Dto;
using System;
using System.Collections.Generic;

namespace Loja.Test.Builder.Dto
{
    public class UsuarioDtoBuilder
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string Role { get; set; }
        public int EstabelecimentoId { get; set; }
        public string EstabelecimentoNomeUrl { get; set; }
        public string EstabelecimentoNome { get; set; }      
        public IEnumerable<EstabelecimentoDto> Estabelecimentos { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public EnderecoDto Endereco { get; set; }

        public UsuarioDtoBuilder()
        {
            Id = new Guid().ToString();
            Nome = "usuario";
            Email = "usuario@teste.com";                     
        }

        public UserDto Build() => new UserDto()
        {
            Id = Id,
            Nome = Nome,
            Email = Email
        };

        public UsuarioDtoBuilder ComID(string ID)
        {
            Id = ID;
            return this;
        }

        public UsuarioDtoBuilder ComCliente1()
        {
            Id = "e36ab3e7-c6a3-4727-b793-3c5aba3baa29";
            Nome = "Cliente 1";
            Email = "cliente@teste.com";
            return this;
        }
    }
}
