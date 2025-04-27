using Modelos.Enuns;

namespace Modelos.ModelosRequest.Licenca
{
    public class AtualizarPlanoLicenca
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public int? QuantidadeDeAcoes { get; set; }
    }
}
