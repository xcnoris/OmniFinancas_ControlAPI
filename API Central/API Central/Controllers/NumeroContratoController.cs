using DataBase.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF.Contrato;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumeroContratoController : ControllerBase
    {
        private readonly DAL<NumeroContratoModel> _dalNumeroContrato;
        private readonly DAL<ContratoModel> _dalContrato;

        public NumeroContratoController(DAL<NumeroContratoModel> dalNumeroContrato, DAL<ContratoModel> dalContrato)
        {
            _dalNumeroContrato = dalNumeroContrato;
            _dalContrato = dalContrato;
        }

        [HttpPost]
        public async Task<ActionResult<NumeroContratoModel>> CriarNumeroContrato([FromBody] NumeroContratoModel request)
        {
            try
            {
                var contrato = await _dalContrato.RecuperarPorAsync(c => c.Id.Equals(request.ContratoId));
                if (contrato is null)
                    return BadRequest("Contrato associado n�o encontrado.");

                request.DataCriacao = DateTime.Now;
                request.DataAtualizacao = DateTime.Now;

                await _dalNumeroContrato.AdicionarAsync(request);
                return Ok(request);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de valida��o: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar n�mero de contrato. {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NumeroContratoModel>>> ListarTodos()
        {
            try
            {
                var lista = await _dalNumeroContrato.ListarAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar os n�meros de contrato. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NumeroContratoModel>> BuscarPorId(int id)
        {
            try
            {
                var numeroContrato = await _dalNumeroContrato.BuscarPorAsync(n => n.Id == id);
                if (numeroContrato is null)
                    return NotFound($"N�mero de contrato com ID {id} n�o encontrado.");

                return Ok(numeroContrato);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar n�mero de contrato. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] NumeroContratoModel atualizado)
        {
            try
            {
                var existente = await _dalNumeroContrato.RecuperarPorAsync(n => n.Id == id);
                if (existente is null)
                    return NotFound($"N�mero de contrato com ID {id} n�o encontrado.");

                existente.Numero = atualizado.Numero;
                existente.NomeInstancia = atualizado.NomeInstancia;
                existente.TokenInstancia = atualizado.TokenInstancia;
                existente.ContratoId = atualizado.ContratoId;
                existente.DataAtualizacao = DateTime.Now;

                await _dalNumeroContrato.AtualizarAsync(existente);
                return Ok("N�mero de contrato atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar n�mero de contrato. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var existente = await _dalNumeroContrato.RecuperarPorAsync(n => n.Id == id);
                if (existente is null)
                    return NotFound($"N�mero de contrato com ID {id} n�o encontrado.");

                await _dalNumeroContrato.DeletarAsync(existente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover n�mero de contrato. {ex.Message}");
            }
        }
    }
}
