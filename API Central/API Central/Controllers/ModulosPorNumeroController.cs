using DataBase.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF;
using Modelos.EF.Contrato;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulosPorNumeroController : ControllerBase
    {
        private readonly DAL<ModulosPorNumeroModel> _dalModulosPorNumero;
        private readonly DAL<NumeroContratoModel> _dalNumeroContrato;
        private readonly DAL<ModulosModel> _dalModulo;

        public ModulosPorNumeroController(
            DAL<ModulosPorNumeroModel> dalModulosPorNumero,
            DAL<NumeroContratoModel> dalNumeroContrato,
            DAL<ModulosModel> dalModulo)
        {
            _dalModulosPorNumero = dalModulosPorNumero;
            _dalNumeroContrato = dalNumeroContrato;
            _dalModulo = dalModulo;
        }

        [HttpPost]
        public async Task<ActionResult<ModulosPorNumeroModel>> Criar([FromBody] ModulosPorNumeroModel request)
        {
            try
            {
                var numero = await _dalNumeroContrato.RecuperarPorAsync(n => n.Id == request.NumeroId);
                if (numero is null)
                    return BadRequest("Número de contrato não encontrado.");

                var modulo = await _dalModulo.RecuperarPorAsync(m => m.Id == request.ModuloId);
                if (modulo is null)
                    return BadRequest("Módulo não encontrado.");

                request.DataCriacao = DateTime.Now;
                request.DataAtualizacao = DateTime.Now;

                await _dalModulosPorNumero.AdicionarAsync(request);
                return Ok(request);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar vínculo. {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModulosPorNumeroModel>>> ListarTodos()
        {
            try
            {
                var lista = await _dalModulosPorNumero.ListarAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar vínculos. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModulosPorNumeroModel>> BuscarPorId(int id)
        {
            try
            {
                var registro = await _dalModulosPorNumero.BuscarPorAsync(m => m.Id == id);
                if (registro is null)
                    return NotFound("Vínculo não encontrado.");

                return Ok(registro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar vínculo. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] ModulosPorNumeroModel atualizado)
        {
            try
            {
                var existente = await _dalModulosPorNumero.RecuperarPorAsync(m => m.Id == id);
                if (existente is null)
                    return NotFound("Vínculo não encontrado.");

                var numero = await _dalNumeroContrato.RecuperarPorAsync(n => n.Id == atualizado.NumeroId);
                if (numero is null)
                    return BadRequest("Número de contrato não encontrado.");

                var modulo = await _dalModulo.RecuperarPorAsync(m => m.Id == atualizado.ModuloId);
                if (modulo is null)
                    return BadRequest("Módulo não encontrado.");

                existente.NumeroId = atualizado.NumeroId;
                existente.ModuloId = atualizado.ModuloId;
                existente.DataAtualizacao = DateTime.Now;

                await _dalModulosPorNumero.AtualizarAsync(existente);
                return Ok("Vínculo atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar vínculo. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var existente = await _dalModulosPorNumero.RecuperarPorAsync(m => m.Id == id);
                if (existente is null)
                    return NotFound("Vínculo não encontrado.");

                await _dalModulosPorNumero.DeletarAsync(existente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover vínculo. {ex.Message}");
            }
        }
    }
}
