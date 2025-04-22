


using Microsoft.AspNet.Identity.EntityFramework;
using Modelos.EF.Revenda;
using Modelos.Enuns;

namespace Modelos.EF.Login
{
    public class UserLoginModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string HashSenha { get; set; }
        public int UsuarioRevendaId { get; set; }
        public virtual UsuariosRevendaModel? UsuarioRevenda { get; set; }
        public TipoUserLogin Tipo_User { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
