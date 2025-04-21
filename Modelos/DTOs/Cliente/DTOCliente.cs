using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.EF.Contrato;
using Modelos.EF.Entidade;
using Modelos.EF.Lincenca;
using Modelos.EF.Revenda;
using Modelos.Enuns;

namespace Modelos.DTOs.Cliente
{
    public class DTOCliente
    {
        public int? Id { get; set; }

        public int EntidadeId { get; set; }

        public int RevendaId { get; set; }

        public Situacao Situacao { get; set; }
    }
}
