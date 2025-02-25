
using DataBase.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF.Revenda;
using Modelos.ModelosRequest.Revenda;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    /// <summary>  
    /// Controlador para gerenciar revendas.  
    /// </summary>  
    [ApiController]
    [Route("api/[controller]")]
    public class RevendaController : ControllerBase
    {
        private readonly DAL<RevendaModel> _dalRevenda;

        /// <summary>  
        /// Construtor do controlador RevendaController.  
        /// </summary>  
        /// <param name="dalRevenda">Dependência de acesso a dados para RevendaModel.</param>  
        public RevendaController(DAL<RevendaModel> dalRevenda)
        {
            _dalRevenda = dalRevenda;
        }

        /// <summary>  
        /// Cria uma nova revenda.  
        /// </summary>  
        /// <param name="revendaRequest">Dados da revenda a ser criada.</param>  
        /// <returns>A revenda criada.</returns>  
        [HttpPost]
        public async Task<ActionResult<RevendaModel>> CriarRevenda([FromBody] CriarRevenda revendaRequest)
        {
            try
            {
                RevendaModel? revendaExistente = await _dalRevenda.RecuperarPorAsync(x => x.CNPJ.Equals(revendaRequest.CNPJ));
                if (revendaExistente is not null) return BadRequest($"Este CNPJ já possui cadastro!");

                RevendaModel novaRevenda = new RevendaModel()
                {
                    EntidadeId = revendaRequest.EntidadeId,
                    Situacao = revendaRequest.Situacao,
                    DataCriacao = DateTime.Now
                };

                // Adicionar a revenda a base de dados  
                await _dalRevenda.AdicionarAsync(novaRevenda);

                // Retorna o objeto adicionado  
                return novaRevenda;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem   
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção  
                return StatusCode(500, $"Erro ao tentar adicionar a entidade. {ex.Message}");
            }
        }
    }
}
