using Modelos.EF;
using Modelos.Enuns;

namespace Modelos.ModelosRequest.Modulos
{
    public class RetornoModulo
    {
        public int Id { get; set; }
        public IdentificacaoModulo Identificacao { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int SoftwareId { get; set; }
        public Situacao Situacao { get; set; }
        public decimal Valor { get; set; }
    }
}
