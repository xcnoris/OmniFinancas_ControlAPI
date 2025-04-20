
using DataBase.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF.Lincenca;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanosLicencaController : ControllerBase
    {
        private readonly DAL<PlanosLicencaModel> _dal;

        public PlanosLicencaController(DAL<PlanosLicencaModel> dal)
        {
            _dal = dal;
        }

        [HttpPost]
        public async Task<ActionResult<PlanosLicencaModel>> Criar([FromBody] PlanosLicencaModel plano)
        {
            try
            {
                plano.DataCriacao = DateTime.Now;
                await _dal.AdicionarAsync(plano);
                return Ok(plano);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar plano de licença. {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanosLicencaModel>>> ListarTodos()
        {
            var planos = await _dal.ListarAsync();
            return Ok(planos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanosLicencaModel>> BuscarPorId(int id)
        {
            var plano = await _dal.BuscarPorAsync(p => p.Id == id);
            if (plano == null)
                return NotFound("Plano não encontrado.");

            return Ok(plano);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] PlanosLicencaModel atualizado)
        {
            var plano = await _dal.RecuperarPorAsync(p => p.Id == id);
            if (plano == null)
                return NotFound("Plano não encontrado.");

            plano.Nome = atualizado.Nome;
            plano.QuantidadeDeAcoes = atualizado.QuantidadeDeAcoes;
            plano.DataAtualizacao = DateTime.Now;

            await _dal.AtualizarAsync(plano);
            return Ok("Plano atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var plano = await _dal.RecuperarPorAsync(p => p.Id == id);
            if (plano == null)
                return NotFound("Plano não encontrado.");

            await _dal.DeletarAsync(plano);
            return NoContent();
        }
    }
}
