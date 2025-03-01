using Modelos.EF.Revenda;
using Modelos.Enuns;
using System.Text.Json.Serialization;

namespace Modelos.EF
{
    public class SoftwaresModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public string Versao { get; set; }
        public int ProprietarioId { get; set; }
        [JsonIgnore] // Evita o ciclo
        public virtual RevendaModel? Proprietario { get; set; }
        public Situacao Situacao{ get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
