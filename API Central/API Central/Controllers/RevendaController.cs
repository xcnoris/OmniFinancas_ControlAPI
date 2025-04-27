
using API_Central.JWTServices;
using DataBase.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF.Revenda;
using Modelos.Enuns;
using Modelos.ModelosRequest.Revenda;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    /// <summary>
    /// Controlador para gerenciar revenda.
    /// </summary>
    [ApiController, Route("api/[controller]"), Authorize(Roles = Roles.Admin)]
    public class RevendaController : ControllerBase
    {
        private readonly DAL<RevendaModel> _dalRevenda;

        /// <summary>
        /// Construtor do controlador de RevendaController.
        /// </summary>
        /// <param name="dalRevenda">Dependência de acesso a dados para Revenda Model.</param>
        public RevendaController(DAL<RevendaModel> dalRevenda)
        {
            _dalRevenda = dalRevenda;
        }

        /// <summary>
        /// Criar uma nova revenda.
        /// </summary>
        /// <param name="revendaRequest">Dados para revenda a ser criada.</param>
        /// <returns>A revenda criada.</returns>
        [HttpPost]
        public async Task<ActionResult<RevendaModel>> CriarRevenda([FromBody] CriarRevenda revendaRequest)
        {
            try
            {
                if (revendaRequest.CNPJ is null)
                    return BadRequest("CNPJ não pode ser nulo!");

                if (revendaRequest.EntidadeId is null)
                    return BadRequest("EntidadeId não pode ser nulo!");

                // Verifica se já existe uma revenda com o mesmo CNPJ
                RevendaModel? revendaExistente = await _dalRevenda.RecuperarPorAsync(x => x.CNPJ.Equals(revendaRequest.CNPJ));
                if (revendaExistente is not null)
                    return BadRequest("Este CNPJ já possui cadastro!");

                // Cria a nova revenda
                RevendaModel novaRevenda = new RevendaModel
                {
                    EntidadeId = revendaRequest.EntidadeId.Value, // Aqui assumimos que EntidadeId não é mais nulo
                    CNPJ = revendaRequest.CNPJ,
                    DataCriacao = DateTime.Now
                };
                if (revendaRequest.Situacao.HasValue)
                    novaRevenda.Situacao = revendaRequest.Situacao.Value;
                else
                    return BadRequest("Situacao nao pode ser nula!");

                await _dalRevenda.AdicionarAsync(novaRevenda);

                // Retorna a revenda criada
                return Ok(novaRevenda);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar adicionar a entidade. {ex.Message}");
            }
        }


        /// <summary>
        /// Buscar todas as revendas.
        /// </summary>
        /// <returns>Lista de revendas.</returns>
        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<IEnumerable<RevendaModel>>> BuscarTodos()
        {
            try
            {
                IEnumerable<RevendaModel> revendas = await _dalRevenda.ListarAsync();
                return Ok(revendas);
            }
            catch (ValidationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar recuperar as revendas. {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar revenda por ID.
        /// </summary>
        /// <param name="id">ID da revenda.</param>
        /// <returns>A revenda encontrada.</returns>
        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<RevendaModel>> BuscarPorId(int id)
        {
            try
            {
                RevendaModel? revenda = await _dalRevenda.BuscarPorAsync(c => c.Id.Equals(id));

                // Retorna 404 Not Found se a revenda não for encontrada
                if (revenda is null) return NotFound($"Não foi encontrada nenhuma revenda com este ID {id}.");

                return Ok(revenda);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar recuperar a revenda. {ex.Message}");
            }   
        }


        /// <summary>
        /// Atualiza uma revenda por ID.
        /// </summary>
        /// <param name="revendaRequest">Dados da revenda a ser atualizada.</param>
        /// <param name="id">ID da revenda.</param>
        /// <returns>A revenda atualizada.</returns>
        [HttpPut("AtualizarPorId/{id}")]
        public async Task<ActionResult<RevendaModel>> AtualizarPorId([FromBody] AtualizarRevenda revendaRequest, int id)
        {
            try
            {
                RevendaModel? revendaExistente = await _dalRevenda.BuscarPorAsync(x => x.Id.Equals(id));
                if (revendaExistente is null) return NotFound($"Não foi encontrada nenhuma revenda com este ID {id}.");

                // Atualizar os campos da revenda existente com os novos dados
                revendaExistente.CNPJ = revendaRequest.CNPJ;

                if (revendaRequest.Situacao.HasValue)
                    revendaExistente.Situacao = revendaRequest.Situacao.Value;
                else
                    return BadRequest("Situacao nao pode ser nula!");

                

                // Chama o método DAL para atualizar a revenda no banco de dados
                await _dalRevenda.AtualizarAsync(revendaExistente);

                // Retorna a revenda atualizada dentro de um OK()
                return Ok(revendaExistente);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar atualizar a revenda. {ex.Message}");
            }   
        }

        /// <summary>
        /// Atualiza a situação de uma revenda por ID.
        /// </summary>
        /// <param name="revendaRequest">Dados para atualizar a situação da revenda.</param>
        /// <param name="id">ID da revenda.</param>
        /// <returns>A revenda atualizada.</returns>
        [HttpPut("AtualizarSituacao/{id}")]
        public async Task<ActionResult<RevendaModel>> AtualizarSituacao([FromBody] AtualizarSituacaoRevenda revendaRequest, int id)
        {
            try
            {
                RevendaModel? revendaExistente = await _dalRevenda.RecuperarPorAsync(x => x.Id.Equals(id));
                if (revendaExistente is null) return NotFound($"Não foi encontrada nenhuma revenda com este ID {id}.");

                // Atualizar a situação da revenda existente
                if (revendaRequest.Situacao.HasValue)
                    revendaExistente.Situacao = revendaRequest.Situacao.Value;
                else
                    return BadRequest("Situacao nao pode ser nula!");

                // Chama o método DAL para atualizar a revenda no banco de dados
                await _dalRevenda.AtualizarAsync(revendaExistente);

                // Retorna a revenda atualizada dentro de um OK()
                return Ok(revendaExistente);
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar atualizar a revenda. {ex.Message}");
            }
        }

        /// <summary>
        /// Remove uma revenda pelo ID.
        /// </summary>
        /// <param name="id">ID da revenda.</param>
        /// <returns>Resultado da remoção.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Remover(int id)
        {
            try
            {
                // Primeiro, recupera a revenda existente pelo ID
                RevendaModel? revendaExistente = await _dalRevenda.BuscarPorAsync(x => x.Id.Equals(id));

                // Retorna 404 Not Found se a revenda não existir
                if (revendaExistente is null) return NotFound();

                try
                {
                    // Chama o método DAL para remover a revenda
                    await _dalRevenda.DeletarAsync(revendaExistente);

                    // Retorna 204 No Content se a remoção foi bem-sucedida
                    return NoContent();
                }
                catch (Exception ex)
                {
                    // Retorna um erro genérico ou detalhado se a remoção falhar
                    return StatusCode(500, $"Erro ao tentar remover a revenda. {ex.Message}");
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar remover a revenda. {ex.Message}");
            }   
        }
    }
}
