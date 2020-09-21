using Microsoft.AspNetCore.Identity;
using System;

namespace Loja.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public override string Id { get => base.Id; set => base.Id = value; }
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
