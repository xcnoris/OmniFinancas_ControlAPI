using Modelos.Enuns;

namespace Modelos.EF.Login
{
    public class RegisterModel
    {
        public string NomeUser { get; set; }
        public string email { get; set; }
        public string Senha { get; set; }
        public int UsuarioRevendaId { get; set; }
        public TipoUserLogin Tipo_User { get; set; }
    }
}
