using Azure.Core;
using DataBase.Data;
using MetodosGerais.ModelsServices;
using MetodosGerais.ModelsServices.EvolutionAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs.Cliente;
using Modelos.DTOs.EvolutionAPI;
using Modelos.EF;
using Modelos.EF.Entidade;
using Modelos.EF.Lincenca;
using Modelos.EF.Revenda;
using Modelos.Enuns;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvolutionAPIController : ControllerBase
    {
        private readonly DAL<ClientesModel> _dalCliente;
        private readonly DAL<PessoaModel> _dalPessoa;
        private readonly DAL<LicencaModel> _dalLicenca;

        public EvolutionAPIController(
            DAL<ClientesModel> dalCliente,
            DAL<PessoaModel> dalPessoa,
            DAL<LicencaModel> dalLicenca)
        {
            _dalCliente = dalCliente;
            _dalPessoa = dalPessoa;
            _dalLicenca = dalLicenca;
        }

        [HttpPost("EnviarMensagem")]
        public async Task<ActionResult<string>> EnviarMensagemPorWhatsapp([FromBody] DTOEnviarMensagem request)
        {
            try
            {
                var licencaExistente = await _dalLicenca.BuscarPorAsync(l => l.ChaveAtivacao == request.ChaveAtivacao);
                if (licencaExistente is null)
                    return NotFound("Licença não encontrada.");

                if (licencaExistente.Situacao != SituacaoLicenca.Ativa)
                    return BadRequest("Licença não está ativa.");
                if (licencaExistente.EnderecoMac != request.EnderecoMac)
                    return BadRequest("Endereço Mac Errado!");

                bool DeuCerto = await HTTPServices.EnviarMensagemViaAPI(request);
                if (DeuCerto)
                {
                    return Ok("Sucesso!");
                }
                else
                {
                    return BadRequest("Mensagem nao foi enviada!");
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao enviar mensagem. {ex.Message}");
            }
        }
    }
}
