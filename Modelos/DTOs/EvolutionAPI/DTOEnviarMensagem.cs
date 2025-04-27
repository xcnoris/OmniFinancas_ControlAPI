using System;
namespace Modelos.DTOs.EvolutionAPI
{
    public class DTOEnviarMensagem
    {
        public string? ChaveAtivacao { get; set; }
        public string? EnderecoMac { get; set; }
        public string? Token { get; set; }
        public string? Instancia { get; set; }
        public string? Numero { get; set; }
        public string? Mensagem { get; set; }
    }
}
