using DataBase.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF.Contrato;
using Modelos.EF.Lincenca;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicencasController : ControllerBase
    {
        private readonly DAL<LicencaModel> _dalLicenca;
        private readonly DAL<ContratoModel> _dalContrato;

        public LicencasController(DAL<LicencaModel> dalLicenca, DAL<ContratoModel> dalContrato)
        {
            _dalLicenca = dalLicenca;
            _dalContrato = dalContrato;
        }

        [HttpPost]
        public async Task<ActionResult<LicencaModel>> Criar([FromBody] LicencaModel licenca)
        {
            var contrato = await _dalContrato.RecuperarPorAsync(c => c.Id == licenca.ContratoId);
            if (contrato is null)
                return BadRequest("Contrato não encontrado.");

            licenca.DataCriacao = DateTime.Now;
            licenca.DataAtualizacao = DateTime.Now;

            await _dalLicenca.AdicionarAsync(licenca);
            return Ok(licenca);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicencaModel>>> Listar()
        {
            var licencas = await _dalLicenca.ListarAsync();
            return Ok(licencas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LicencaModel>> BuscarPorId(int id)
        {
            var licenca = await _dalLicenca.BuscarPorAsync(l => l.Id == id);
            if (licenca is null)
                return NotFound("Licença não encontrada.");

            return Ok(licenca);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] LicencaModel atualizada)
        {
            var existente = await _dalLicenca.RecuperarPorAsync(l => l.Id == id);
            if (existente is null)
                return NotFound("Licença não encontrada.");

            var contrato = await _dalContrato.RecuperarPorAsync(c => c.Id == atualizada.ContratoId);
            if (contrato is null)
                return BadRequest("Contrato não encontrado.");

            existente.ChaveAtivacao = atualizada.ChaveAtivacao;
            existente.ContratoId = atualizada.ContratoId;
            existente.QuantidadeAcoesDisponivel = atualizada.QuantidadeAcoesDisponivel;
            existente.Situacao = atualizada.Situacao;
            existente.DataAtualizacao = DateTime.Now;

            await _dalLicenca.AtualizarAsync(existente);
            return Ok("Licença atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var existente = await _dalLicenca.RecuperarPorAsync(l => l.Id == id);
            if (existente is null)
                return NotFound("Licença não encontrada.");

            await _dalLicenca.DeletarAsync(existente);
            return NoContent();
        }
    }
}
