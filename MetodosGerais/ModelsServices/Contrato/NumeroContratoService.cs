using Modelos.DTOs.NumeroContrato.InPut;
using Modelos.EF.Contrato;

namespace MetodosGerais.ModelsServices.Contrato
{
    public class NumeroContratoService
    {
        public static IEnumerable<DTONumeroContrato> InstanciarListaDTONumeroContrato(IEnumerable<NumeroContratoModel> ListNumeroContrato)
        {
            return ListNumeroContrato.Select(NumeroContrato => new DTONumeroContrato
            {
                Id = NumeroContrato.Id,
                ContratoId = NumeroContrato.ContratoId,
                Numero = NumeroContrato.Numero,
                NomeInstancia = NumeroContrato.NomeInstancia,
                TokenInstancia = NumeroContrato.TokenInstancia,
            }).ToList();
        }
        public static DTONumeroContrato InstanciarDTONumeroContrato(NumeroContratoModel NumeroContrato)
        {
            return new DTONumeroContrato()
            {
                Id = NumeroContrato.Id,
                ContratoId = NumeroContrato.ContratoId,
                Numero = NumeroContrato.Numero,
                NomeInstancia = NumeroContrato.NomeInstancia,
                TokenInstancia = NumeroContrato.TokenInstancia,
            };
        }

        public static NumeroContratoModel InstanciarNumeroContrato(NumeroContratoModel existente, DTONumeroContrato DTOnumeroContrato)
        {
            existente.ContratoId = DTOnumeroContrato.ContratoId;
            existente.Numero = DTOnumeroContrato.Numero;
            existente.NomeInstancia = DTOnumeroContrato.NomeInstancia;
            existente.TokenInstancia = DTOnumeroContrato.TokenInstancia;
            existente.DataAtualizacao = DateTime.Now;

            return existente;
        }
    }
}
