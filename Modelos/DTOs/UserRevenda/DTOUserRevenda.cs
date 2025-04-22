using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.EF.Entidade;
using Modelos.EF.Revenda;
using Modelos.Enuns;

namespace Modelos.DTOs.UserRevenda
{
    public class DTOUserRevenda
    {
        public int? Id { get; set; }

        public int RevendaId { get; set; }

        // Nesse caso vai ser uma entidade com o tipo Fisica
        public int EntidadeId { get; set; }
    }
}
