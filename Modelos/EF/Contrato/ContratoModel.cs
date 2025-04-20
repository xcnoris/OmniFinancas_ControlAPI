using Modelos.EF;
using Modelos.EF.Lincenca;
using Modelos.Enuns;

namespace Modelos.EF.Contrato
{
    public class ContratoModel
    {
        public int Id { get; set; }
        public int Tipo_PlanoId { get; set; }
        public virtual PlanosLicencaModel? Tipo_Plano { get; set; }
        public int ClienteFinalId { get; set; }
        public virtual ClientesModel? ClienteFinal { get; set; }

        public int? LicencaId { get; set; }
        public virtual LicencaModel? Licenca { get; set; }


        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }


        public List<NumeroContratoModel> NumerosDoContrato { get; set; } = new List<NumeroContratoModel>();
    }
}
