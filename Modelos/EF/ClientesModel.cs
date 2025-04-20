using Modelos.EF.Contrato;
using Modelos.EF.Entidade;
using Modelos.EF.Lincenca;
using Modelos.EF.Revenda;
using Modelos.Enuns;

namespace Modelos.EF
{
    public class ClientesModel
    {
        public int Id { get; set; }

        public int EntidadeId { get; set; }
        public virtual PessoaModel? Entidade { get; set; }

        public int RevendaId { get; set; }
        public virtual RevendaModel? Revenda { get; set; }

        public Situacao Situacao { get; set; }
        public DateTime DataCriacao { get; set; }


        public List<LicencaModel> Licencas { get; set; } = new();
        public List<ContratoModel> Contratos { get; set; } = new();

    }
}
