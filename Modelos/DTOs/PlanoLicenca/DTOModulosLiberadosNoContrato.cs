
using Modelos.Enuns;

namespace Modelos.DTOs.PlanoLicenca
{
    public class DTOModulosLiberadosNoContrato
    {
        public string? Numero { get; set; }
        public IdentificacaoModulo? IdentificadorModulo { get; set; }

        public string? NomeInstancia { get; set; }
        public string? TokenInstancia { get; set; }
    }
}
