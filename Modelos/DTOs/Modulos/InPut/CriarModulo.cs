using Modelos.EF;
using Modelos.Enuns;

namespace Modelos.DTOs.Modulos.InPut
{
    public class CriarModulo
    {
        public IdentificacaoModulo? Identificao { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int? SoftwareId { get; set; }
        public decimal? Valor { get; set; }
        public Situacao? Situacao { get; set; }

    }
}
