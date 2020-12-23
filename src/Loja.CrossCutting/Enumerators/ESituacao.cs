using System.ComponentModel;

namespace Loja.CrossCutting.Enumerators
{
    public enum ESituacao
    {
        [Description("Ativo")]
        ATIVO = 1,

        [Description("Pendente")]
        PENDENTE = 2,

        [Description("Finalizado")]
        FINALIZADO = 3,

        [Description("Cancelado")]
        CANCELADO = 4,

        [Description("Inativo")]
        INATIVO = 5

    }
}
