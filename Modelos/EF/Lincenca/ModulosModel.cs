using Modelos.Enuns;

namespace Modelos.EF
{
    public class ModulosModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int SoftwareId { get; set; }
        public virtual SoftwaresModel? Software { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
