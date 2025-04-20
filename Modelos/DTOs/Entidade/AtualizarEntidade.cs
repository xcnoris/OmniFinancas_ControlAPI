using Modelos.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.ModelosRequest.Entidade
{
    public class AtualizarEntidade
    {
        public int Id { get; set; }
        public Tipo_Entidade Tipo_Entidade { get; set; }
        public string Nome { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
        public Situacao Situacao { get; set; }
    }
}
