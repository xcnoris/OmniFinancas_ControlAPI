using API_Central.JWTServices;
using DataBase.Data;
using MetodosGerais.ModelsServices.Contrato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs.Contrato;
using Modelos.EF;
using Modelos.EF.Contrato;
using Modelos.EF.Lincenca;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly DAL<ContratoModel> _dalContrato;
        private readonly DAL<ClientesModel> _dalCliente;
        private readonly DAL<PlanosLicencaModel> _dalPlano;

        public ContratoController(
            DAL<ContratoModel> dalContrato,
            DAL<ClientesModel> dalCliente,
            DAL<PlanosLicencaModel> dalPlano)
        {
            _dalContrato = dalContrato;
            _dalCliente = dalCliente;
            _dalPlano = dalPlano;
        }

        [HttpPost, Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<ContratoModel>> CriarContrato([FromBody] DTOContrato request)
        {
            try
            {
                var cliente = await _dalCliente.RecuperarPorAsync(c => c.Id.Equals(request.ClienteFinalId));
                if (cliente is null) return BadRequest("Cliente não encontrado.");

                var plano = await _dalPlano.RecuperarPorAsync(p => p.Id.Equals(request.Tipo_PlanoId));
                if (plano is null) return BadRequest("Plano de licença não encontrado.");


                ContratoModel NovoContrato = ContratoService.InstanciarContrato(new ContratoModel(), request);
                NovoContrato.DataCriacao = DateTime.Now;

                await _dalContrato.AdicionarAsync(NovoContrato);
                return Ok(request);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar criar o contrato. {ex.Message}");
            }
        }

        [HttpGet, Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<IEnumerable<ContratoModel>>> Listar()
        {
            try
            {
                var contratos = await _dalContrato.ListarAsync();
                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar contratos. {ex.Message}");
            }
        }

        [HttpGet("{id}"), Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<ContratoModel>> BuscarPorId(int id)
        {
            try
            {
                var contrato = await _dalContrato.BuscarPorAsync(c => c.Id == id);
                if (contrato is null) return NotFound($"Contrato com ID {id} não encontrado.");

                return Ok(contrato);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar contrato. {ex.Message}");
            }
        }

        [HttpPut("{id}"), Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult> AtualizarContrato(int id, [FromBody] ContratoModel contratoAtualizado)
        {
            try
            {
                var contratoExistente = await _dalContrato.RecuperarPorAsync(c => c.Id == id);
                if (contratoExistente is null) return NotFound($"Contrato com ID {id} não encontrado.");

                contratoExistente.ClienteFinalId = contratoAtualizado.ClienteFinalId;
                contratoExistente.Tipo_PlanoId = contratoAtualizado.Tipo_PlanoId;
                contratoExistente.DataAtualizacao = DateTime.Now;
                contratoExistente.Valor = contratoAtualizado.Valor;

                // Aqui pode-se adicionar lógica para atualizar os `NumerosDoContrato` também, se necessário.

                await _dalContrato.AtualizarAsync(contratoExistente);
                return Ok("Contrato atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar contrato. {ex.Message}");
            }
        }

        [HttpDelete("{id}"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var contrato = await _dalContrato.RecuperarPorAsync(c => c.Id == id);
                if (contrato is null) return NotFound($"Contrato com ID {id} não encontrado.");

                await _dalContrato.DeletarAsync(contrato);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover contrato. {ex.Message}");
            }
        }
    }
}
