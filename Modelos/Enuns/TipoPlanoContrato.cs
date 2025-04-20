using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Enuns
{
    public enum TipoPlanoContrato
    {
        [Description("Plano Basico")]
        Basic = 1,

        [Description("Plano Padrão")]
        Padrao = 2,

        [Description("Plano Full")]
        Full = 3

    }
}
