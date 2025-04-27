using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.DTOs.Cliente;
using Modelos.DTOs.Contrato;
using Modelos.EF;
using Modelos.EF.Contrato;
using Modelos.Enuns;

namespace MetodosGerais.ModelsServices
{
    public class ClienteService
    {
        public static IEnumerable<DTOCliente> InstanciarListaDTOCliente(IEnumerable<ClientesModel> ListCliente)
        {
            return ListCliente.Select(NumeroContrato => new DTOCliente
            {
                Id = NumeroContrato.Id,
                EntidadeId = NumeroContrato.EntidadeId,
                RevendaId = NumeroContrato.RevendaId,
                Situacao = NumeroContrato.Situacao,
            }).ToList();
        }
        public static DTOCliente InstanciarDTOCliente(ClientesModel Cliente)
        {
            return new DTOCliente()
            {
                Id = Cliente.Id,
                EntidadeId = Cliente.EntidadeId,
                RevendaId = Cliente.RevendaId,
                Situacao = Cliente.Situacao,
            };
        }

        public static ClientesModel InstanciarCliente(ClientesModel existente, DTOCliente DTOCliente)
        {
            existente.Id = Convert.ToInt32(DTOCliente.Id);
            existente.EntidadeId = DTOCliente.EntidadeId.Value;
            existente.RevendaId = DTOCliente.RevendaId.Value;
            existente.Situacao = DTOCliente.Situacao.Value;
            existente.DataCriacao = DateTime.Now;
            

            return existente;
        }
    }
}
