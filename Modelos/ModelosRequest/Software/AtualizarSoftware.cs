using Modelos.EF.Revenda;
using Modelos.Enuns;

namespace Modelos.ModelosRequest.Software
{
    public class AtualizarSoftware
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public string Versao { get; set; }
        public int ProprietarioId { get; set; }
        public Situacao Situacao { get; set; }
       
    }
}
 