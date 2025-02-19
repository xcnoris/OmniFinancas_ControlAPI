using Modelos.EF.Entidade;
using Modelos.EF.Login;
using Modelos.Enuns;

namespace Modelos.EF.Revenda
{
    public class UsuariosRevendaModel
    {
        public UsuariosRevendaModel()
        {
            if (Entidade != null && Entidade.TipoEntidade != Tipo_Entidade.Fisica)
            {
                throw new InvalidOperationException("Um Usuario de Revenda deve ser do tipo Física.");
            }
        }

        public int Id { get; set; }

        public int RevendaId { get; set; }
        public virtual RevendaModel? Revenda { get; set; }

        // Nesse caso vai ser uma entidade com o tipo Fisica
        public int EntidadeId { get; set; }
        public virtual EntidadeModel? Entidade { get; set; }

        //public int UsuarioLoginId { get; set; }
        //public virtual UserLoginModel? UsuarioLogin { get; set; }

        public Situacao Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
