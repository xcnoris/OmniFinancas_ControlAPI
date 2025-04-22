using Modelos.EF.Contrato;
using Modelos.EF.Revenda;
using Modelos.Enuns;

namespace Modelos.EF.Lincenca
{
    public class LicencaModel
    {
        public int Id { get; set; }

        
        /// <summary>
        /// Código único da licença.
        /// </summary>
        public string ChaveAtivacao { get; set; }

        public string EnderecoMac { get; set; }
        public int ContratoId { get; set; }
        public virtual ContratoModel? Contrato { get; set; }

        public int QuantidadeAcoesDisponivel { get; set; }
        /// <summary>
        /// Situação da licença (ativa, expirada, cancelada).
        /// </summary>
        public SituacaoLicenca Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
