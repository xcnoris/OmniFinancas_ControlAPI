
using Modelos.Enuns;

namespace Modelos.ModelosRequest.Modulos
{
    public class AtualizarModulo
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int SoftwareId { get; set; }
        public Situacao Situacao { get; set; }
    }
}
