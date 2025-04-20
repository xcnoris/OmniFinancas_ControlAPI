using Modelos.EF.Entidade;
using Modelos.EF.Lincenca;
using Modelos.Enuns;
using System.Reflection.Metadata;

namespace Modelos.EF.Revenda
{
    public class RevendaModel
    {
        public RevendaModel()
        {
            if (Entidade != null && Entidade.TipoEntidade != Tipo_Entidade.Juridica)
            {
                throw new InvalidOperationException("Uma revenda deve ser do tipo Jurídica.");
            }
        }


        public int Id { get; set; }

        // Nesse caso vai ser uma entidade com o tipo Juridica
        public int EntidadeId { get; set; }
        public virtual PessoaModel? Entidade { get; set; }
        public string CNPJ { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime DataCriacao { get; set; }



        public List<UsuariosRevendaModel> UsuarioRevendaList { get; set; } = new();
        public List<SoftwaresModel> SoftwaresList { get; set; } = new();
        public List<PlanosLicencaModel> PlanosLicencaList { get; set; } = new();
        public List<ClientesModel> ClientesList { get; set; } = new();

    }
}
