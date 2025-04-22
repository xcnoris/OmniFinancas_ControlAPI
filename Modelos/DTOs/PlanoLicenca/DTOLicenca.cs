using Modelos.Enuns;

namespace Modelos.DTOs.PlanoLicenca
{
    public class DTOLicenca
    {
        public int? Id { get; set; }


        /// <summary>
        /// Código único da licença.
        /// </summary>
        public string? ChaveAtivacao { get; set; }

        public int ContratoId { get; set; }

        public string EnderecoMac { get; set; }
        /// <summary>
        /// Situação da licença (ativa, expirada, cancelada).
        /// </summary>
        public SituacaoLicenca Situacao { get; set; }

    }
}
