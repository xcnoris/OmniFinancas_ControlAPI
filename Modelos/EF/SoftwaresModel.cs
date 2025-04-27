using Modelos.EF.Revenda;
using Modelos.Enuns;

namespace Modelos.EF
{
    public class SoftwaresModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Versao { get; set; }
        public int ProprietarioId { get; set; }
        public virtual RevendaModel? Proprietario { get; set; }
        public Situacao Situacao{ get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
