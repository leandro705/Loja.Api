using System.Collections.Generic;
using System.Security.Claims;

namespace Loja.CrossCutting.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<Claim> Claims { get; set; }        
        public bool IsGoogle { get; set; }
        public bool IsFacebook { get; set; }
        public string Login { get; set; }
    }
}
