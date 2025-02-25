using Modelos.Enuns;

namespace Modelos.ModelosRequest.Licenca
{
    public class AtualizarPlanoLicenca
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DuracaoMeses { get; set; }
        public int QuantidadeUsuarios { get; set; }
        public Situacao Situacao { get; set; }
    }
}
