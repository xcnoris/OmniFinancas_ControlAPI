using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.EF.Lincenca;
using Modelos.EF;

namespace Modelos.DTOs.Contrato
{
    public class DTOContrato
    {
        public int? Id { get; set; }
        public int? Tipo_PlanoId { get; set; }
        public int? ClienteFinalId { get; set; }
        public Decimal? Valor { get; set; }

    }
}
