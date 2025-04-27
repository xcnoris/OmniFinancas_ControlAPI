using Modelos.EF.Entidade;
using Modelos.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.ModelosRequest.Revenda
{
    public class CriarRevenda
    {
        public int? EntidadeId { get; set; }
        public string? CNPJ { get; set; }
        public Situacao? Situacao { get; set; }
    }
}
