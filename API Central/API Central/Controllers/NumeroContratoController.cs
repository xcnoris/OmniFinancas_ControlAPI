using API_Central.JWTServices;
using DataBase.Data;
using MetodosGerais.ModelsServices.Contrato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs.NumeroContrato.InPut;
using Modelos.EF.Contrato;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class NumeroContratoController : ControllerBase
    {
        private readonly DAL<NumeroContratoModel> _dalNumeroContrato;
        private readonly DAL<ContratoModel> _dalContrato;

        public NumeroContratoController(DAL<NumeroContratoModel> dalNumeroContrato, DAL<ContratoModel> dalContrato)
        {
            _dalNumeroContrato = dalNumeroContrato;
            _dalContrato = dalContrato;
        }

        [HttpPost, Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<string>> CriarNumeroContrato([FromBody] DTONumeroContrato request)
        {
            try
            {
                var contrato = await _dalContrato.RecuperarPorAsync(c => c.Id.Equals(request.ContratoId));
                if (contrato is null) return BadRequest("Contrato associado não encontrado.");

                NumeroContratoModel NumeroContrato = new NumeroContratoModel()
                {
                    ContratoId = request.ContratoId.Value,
                    Numero =  request.Numero,
                    NomeInstancia = request.NomeInstancia,
                    TokenInstancia = request.TokenInstancia,
                    DataCriacao = DateTime.Now,
                };

                await _dalNumeroContrato.AdicionarAsync(NumeroContrato);

                return Ok("Numero Contrato Criado com Sucesso!");
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar número de contrato. {ex.Message}");
            }
        }

        [HttpGet, Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<IEnumerable<NumeroContratoModel>>> ListarTodos()
        {
            try
            {
                var lista = await _dalNumeroContrato.ListarAsync();
                return Ok(NumeroContratoService.InstanciarListaDTONumeroContrato(lista));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar os números de contrato. {ex.Message}");
            }
        }

        [HttpGet("{id}"), Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<DTONumeroContrato>> BuscarPorId(int id)
        {
            try
            {
                NumeroContratoModel? NumeroContrato = await _dalNumeroContrato.BuscarPorAsync(n => n.Id == id);
                if (NumeroContrato is null) return NotFound($"Número de contrato com ID {id} não encontrado.");

                return Ok(NumeroContratoService.InstanciarDTONumeroContrato(NumeroContrato));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar número de contrato. {ex.Message}");
            }
        }

        [HttpPut("{id}"), Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult> Atualizar(int id, [FromBody] DTONumeroContrato atualizado)
        {
            try
            {
                NumeroContratoModel? existente = await _dalNumeroContrato.RecuperarPorAsync(n => n.Id == id);
                if (existente is null) return NotFound($"Número de contrato com ID {id} não encontrado.");

                NumeroContratoModel ExistenteAtualizado = NumeroContratoService.InstanciarNumeroContrato(existente, atualizado);
              
                await _dalNumeroContrato.AtualizarAsync(ExistenteAtualizado);
                return Ok("Número de contrato atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar número de contrato. {ex.Message}");
            }
        }

        [HttpDelete("{id}"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var existente = await _dalNumeroContrato.RecuperarPorAsync(n => n.Id == id);
                if (existente is null) return NotFound($"Número de contrato com ID {id} não encontrado.");

                await _dalNumeroContrato.DeletarAsync(existente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover número de contrato. {ex.Message}");
            }
        }
    }
}
