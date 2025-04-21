using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.DTOs.Contrato;
using Modelos.DTOs.NumeroContrato.InPut;
using Modelos.EF.Contrato;

namespace MetodosGerais.ModelsServices.Contrato
{
    public class ContratoService
    {
        public static IEnumerable<DTOContrato> InstanciarListaDTOContrato(IEnumerable<ContratoModel> ListNumeroContrato)
        {
            return ListNumeroContrato.Select(NumeroContrato => new DTOContrato
            {
                Id = NumeroContrato.Id,
                Tipo_PlanoId = NumeroContrato.Tipo_PlanoId,
                ClienteFinalId = NumeroContrato.ClienteFinalId,
                Valor = NumeroContrato.Valor,
            }).ToList();
        }
        public static DTOContrato InstanciarDTOContrato(ContratoModel NumeroContrato)
        {
            return new DTOContrato()
            {
                Id = NumeroContrato.Id,
                Tipo_PlanoId = NumeroContrato.Tipo_PlanoId,
                ClienteFinalId = NumeroContrato.ClienteFinalId,
                Valor = NumeroContrato.Valor,
            };
        }

        public static ContratoModel InstanciarContrato(ContratoModel existente, DTOContrato DTOContrato)
        {
            existente.Id = Convert.ToInt32(DTOContrato.Id);
            existente.Tipo_PlanoId = DTOContrato.Tipo_PlanoId;
            existente.ClienteFinalId = DTOContrato.ClienteFinalId;
            existente.Valor = DTOContrato.Valor;
            existente.DataAtualizacao = DateTime.Now;

            return existente;
        }
    }
}
