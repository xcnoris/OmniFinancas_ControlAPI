
namespace Modelos.EF.Contrato
{
    public class ModulosPorNumeroModel
    {
        public int Id { get; set; }
        public int NumeroId { get; set; }
        public virtual NumeroContratoModel? Numero { get; set; }
        public int ModuloId { get; set; }
        public virtual ModulosModel? Modulo { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
