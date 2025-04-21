using DataBase.Data;
using MetodosGerais.ModelsServices;
using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs.Cliente;
using Modelos.EF;
using Modelos.EF.Entidade;
using Modelos.EF.Lincenca;
using Modelos.EF.Revenda;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly DAL<ClientesModel> _dalCliente;
        private readonly DAL<PessoaModel> _dalPessoa;
        private readonly DAL<RevendaModel> _dalRevenda;

        public ClientesController(
            DAL<ClientesModel> dalCliente,
            DAL<PessoaModel> dalPessoa,
            DAL<RevendaModel> dalRevenda)
        {
            _dalCliente = dalCliente;
            _dalPessoa = dalPessoa;
            _dalRevenda = dalRevenda;
        }

        [HttpPost]
        public async Task<ActionResult<ClientesModel>> CriarCliente([FromBody] DTOCliente ClienteRequest)
        {
            try
            {
                var entidade = await _dalPessoa.RecuperarPorAsync(e => e.Id == ClienteRequest.EntidadeId);
                if (entidade is null)
                    return BadRequest("Entidade (Pessoa) não encontrada.");

                var revenda = await _dalRevenda.RecuperarPorAsync(r => r.Id == ClienteRequest.RevendaId);
                if (revenda is null)
                    return BadRequest("Revenda não encontrada.");

                ClientesModel NovoCliente = ClienteService.InstanciarCliente(new ClientesModel(), ClienteRequest);


                await _dalCliente.AdicionarAsync(NovoCliente);
                return Ok(ClienteRequest);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar cliente. {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesModel>>> Listar()
        {
            try
            {
                var clientes = await _dalCliente.ListarAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar clientes. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesModel>> BuscarPorId(int id)
        {
            try
            {
                var cliente = await _dalCliente.BuscarPorAsync(c => c.Id == id);
                if (cliente is null)
                    return NotFound($"Cliente com ID {id} não encontrado.");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar cliente. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarCliente(int id, [FromBody] ClientesModel clienteAtualizado)
        {
            try
            {
                var clienteExistente = await _dalCliente.RecuperarPorAsync(c => c.Id == id);
                if (clienteExistente is null)
                    return NotFound($"Cliente com ID {id} não encontrado.");

                clienteExistente.EntidadeId = clienteAtualizado.EntidadeId;
                clienteExistente.RevendaId = clienteAtualizado.RevendaId;
                clienteExistente.Situacao = clienteAtualizado.Situacao;

                await _dalCliente.AtualizarAsync(clienteExistente);
                return Ok("Cliente atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar cliente. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var cliente = await _dalCliente.RecuperarPorAsync(c => c.Id == id);
                if (cliente is null)
                    return NotFound($"Cliente com ID {id} não encontrado.");

                await _dalCliente.DeletarAsync(cliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover cliente. {ex.Message}");
            }
        }
    }
}
