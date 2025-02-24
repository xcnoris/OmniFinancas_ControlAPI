using Modelos.EF.Revenda;
using Modelos.Enuns;

namespace Modelos.EF.Lincenca
{
    public class LicencaModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Identifica o software vinculado a esta licença.
        /// </summary>
        public int SoftwareId { get; set; }
        public virtual SoftwaresModel? Software { get; set; }

        /// <summary>
        /// Cliente que possui esta licença.
        /// </summary>
        public int ClienteId { get; set; }
        public virtual ClientesModel? Cliente { get; set; }

        /// <summary>
        /// Plano de licença associado.
        /// </summary>
        public int PlanoLicencaId { get; set; }
        public virtual PlanosLicencaModel? PlanoLicenca { get; set; }

        /// <summary>
        /// Código único da licença.
        /// </summary>
        public string ChaveAtivacao { get; set; }

        /// <summary>
        /// Data de início da licença.
        /// </summary>
        public DateTime DataInicio { get; set; }

        /// <summary>
        /// Data de expiração da licença.
        /// </summary>
        public DateTime? DataExpiracao { get; set; }

        /// <summary>
        /// Situação da licença (ativa, expirada, cancelada).
        /// </summary>
        public SituacaoLicenca Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
