using System.Collections.Generic;

namespace Loja.Domain.Interfaces.Validators
{
    public interface IValidator
    {
        bool Validate();

        List<string> Mensagens { get; }
    }
}
