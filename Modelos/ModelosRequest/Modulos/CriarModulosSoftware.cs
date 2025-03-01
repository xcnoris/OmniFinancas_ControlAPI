using Modelos.EF;
using Modelos.Enuns;

namespace Modelos.ModelosRequest.Licenca
{
    public class CriarModulosSoftware
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int SoftwareId { get; set; }
        public Situacao Situacao { get; set; }
    }
}
