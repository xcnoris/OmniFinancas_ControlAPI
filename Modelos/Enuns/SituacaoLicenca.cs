using System.ComponentModel;

namespace Modelos.Enuns
{
    public enum SituacaoLicenca
    {
        [Description("Ativa")]
        Ativa = 1,

        [Description("Bloqueada")]
        Bloqueada = 2,

        [Description("Expirada")]
        Expirada = 3,

        [Description("Cancelada")]
        Cancelada = 4
    }
}
