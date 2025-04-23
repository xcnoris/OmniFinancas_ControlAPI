using API_Central.JWTServices;
using DataBase.Data;
using MetodosGerais.ModelsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs.PlanoLicenca;
using Modelos.EF;
using Modelos.EF.Contrato;
using Modelos.EF.Lincenca;
using Modelos.Enuns;

namespace API_Central.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class LicencasController : ControllerBase
    {
        private readonly DAL<LicencaModel> _dalLicenca;
        private readonly DAL<ContratoModel> _dalContrato;
        private readonly DAL<NumeroContratoModel> _dalNumeroContrato;
        private readonly DAL<ModulosPorNumeroModel> _dalModuloNumero;
        private readonly DAL<ModulosModel> _dalModulo;

        public LicencasController(
            DAL<LicencaModel> dalLicenca,
            DAL<ContratoModel> dalContrato,
            DAL<NumeroContratoModel> dalNumeroContrato,
            DAL<ModulosPorNumeroModel> ModuloNumero,
            DAL<ModulosModel> dalModulo
            )
        {
            _dalLicenca = dalLicenca;
            _dalContrato = dalContrato;
            _dalNumeroContrato = dalNumeroContrato;
            _dalModuloNumero = ModuloNumero;
            _dalModulo = dalModulo;
        }

        [HttpPost, Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<DTOLicenca>> Criar([FromBody] DTOLicenca licenca)
        {
            var contrato = await _dalContrato.RecuperarPorAsync(c => c.Id == licenca.ContratoId);
            if (contrato is null) return BadRequest("Contrato não encontrado.");

            LicencaModel novaLicenca = LicencaService.InstanciarNumeroContrato(new LicencaModel(), licenca);
            novaLicenca.DataCriacao = DateTime.Now;
            novaLicenca.EnderecoMac = licenca.EnderecoMac;
            novaLicenca.ChaveAtivacao = LicencaService.GerarChaveAtivacao(licenca.ContratoId);

            await _dalLicenca.AdicionarAsync(novaLicenca);
            return Ok(licenca);
        }


        [HttpGet, Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<IEnumerable<LicencaModel>>> Listar()
        {
            var licencas = await _dalLicenca.ListarAsync();
            return Ok(licencas);
        }


        [HttpGet("{id}"), Authorize(Roles = Roles.Revenda)]
        public async Task<ActionResult<LicencaModel>> BuscarPorId(int id)
        {
            var licenca = await _dalLicenca.BuscarPorAsync(l => l.Id == id);
            if (licenca is null)
                return NotFound("Licença não encontrada.");

            return Ok(licenca);
        }

        [HttpPut("{id}"), Authorize(Roles = Roles.Revenda)]
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
            existente.EnderecoMac = atualizada.EnderecoMac;
            existente.Situacao = atualizada.Situacao;
            existente.DataAtualizacao = DateTime.Now;

            await _dalLicenca.AtualizarAsync(existente);
            return Ok("Licença atualizada com sucesso.");
        }

        
        [HttpDelete("{id}"), Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Remover(int id)
        {
            var existente = await _dalLicenca.RecuperarPorAsync(l => l.Id == id);
            if (existente is null)
                return NotFound("Licença não encontrada.");

            await _dalLicenca.DeletarAsync(existente);
            return NoContent();
        }

        [HttpGet("BuscarModulosLiberados")]
        public async Task<ActionResult<IEnumerable<DTOModulosLiberadosNoContrato>>> BuscarModulosPorLinca([FromBody]  DTOBuscarModulosPorNumero request)
        {
            var licencaExistente = await _dalLicenca.BuscarPorAsync(l => l.ChaveAtivacao == request.ChaveAtivacao);
            if (licencaExistente is null)
                return NotFound("Licença não encontrada.");

            if (licencaExistente.Situacao != SituacaoLicenca.Ativa)
                return BadRequest("Licença não está ativa.");
            if (licencaExistente.EnderecoMac != request.EnderecoMac)
                return BadRequest("Endereço Mac Errado!");


            var listaNumeros = (await _dalNumeroContrato.RecuperarTodosPorAsync(n => n.ContratoId == licencaExistente.ContratoId)).ToList();
            var numeroIds = listaNumeros.Select(n => n.Id).ToList();

            var modulosPorNumero = (await _dalModuloNumero.RecuperarTodosPorAsync(m => numeroIds.Contains(m.NumeroId))).ToList();
            var moduloIds = modulosPorNumero.Select(m => m.ModuloId).Distinct().ToList();

            var modulos = (await _dalModulo.RecuperarTodosPorAsync(m => moduloIds.Contains(m.Id))).ToList();

            var resultado = modulosPorNumero
                .Join(listaNumeros, mpn => mpn.NumeroId, num => num.Id, (mpn, num) => new { mpn, num })
                .Join(modulos, x => x.mpn.ModuloId, mod => mod.Id, (x, mod) => new DTOModulosLiberadosNoContrato
                {
                    Numero = x.num.Numero,
                    IdentificadorModulo = mod.Identificacao,
                    NomeInstancia = x.num.NomeInstancia,
                    TokenInstancia = x.num.TokenInstancia
                })
                .ToList();

            return Ok(resultado);
        }



    }
}
