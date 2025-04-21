using DataBase.Data;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF.Lincenca;
using Modelos.ModelosRequest.Licenca;
using System.ComponentModel.DataAnnotations;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoLicencaController : ControllerBase
    {
        private readonly DAL<PlanosLicencaModel> _dalPlanoLicenca;
        public PlanoLicencaController(
            DAL<PlanosLicencaModel> dalPlanoLicenca
            )
        {
            _dalPlanoLicenca = dalPlanoLicenca;

        }


        [HttpPost]
        public async Task<ActionResult<PlanosLicencaModel>> CriarPlanoLicenca([FromBody] CriarPlanoLicenca PlanoLicencaRequest)
        {
            try
            {
                PlanosLicencaModel? RecursoExistente = await _dalPlanoLicenca.RecuperarPorAsync(x => x.Nome.Equals(PlanoLicencaRequest.Nome));
                if (RecursoExistente is not null) return BadRequest($"Nome do PLano de Licença já existe no banco de Dados!");

                PlanosLicencaModel NovoPlanoLicenca = new PlanosLicencaModel()
                {
                    Nome = PlanoLicencaRequest.Nome,
                    QuantidadeDeAcoes = PlanoLicencaRequest.QuantidadesDeAcoes,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now
                };

                // Adicionar o recurso a base de dados
                await _dalPlanoLicenca.AdicionarAsync(NovoPlanoLicenca);

                // Retorna o objeto adicionado
                return NovoPlanoLicenca;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar adicionar o recurso. {ex.Message}");
            }
        }

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<IEnumerable<PlanosLicencaModel>>> GetXmls()
        {
            try
            {
                IEnumerable<PlanosLicencaModel?> planoslicenca = await _dalPlanoLicenca.ListarAsync();
                return Ok(planoslicenca);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar buscar o recurso. {ex.Message}");
            }
        }

        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<PlanosLicencaModel>> BuscarPorId(int id)
        {
            try
            {
                PlanosLicencaModel? RecursoExistente = await _dalPlanoLicenca.BuscarPorAsync(c => c.Id.Equals(id));

                // Retorna 404 Not Found se a entidade não for compatível com a contabilidade
                if (RecursoExistente is null) return NotFound($"Não foi encontrado nenhum Plano com este ID {id}.");

                return Ok(RecursoExistente);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar buscar o recurso. {ex.Message}");
            }
        }



        [HttpPut("AtualizarPorId/{id}")]
        public async Task<ActionResult<PlanosLicencaModel>> AtualizarPorId([FromBody] AtualizarPlanoLicenca PlanosLicencaRequest, int id)
        {
            try
            {
                PlanosLicencaModel? RecursoExistente = await _dalPlanoLicenca.RecuperarPorAsync(x => x.Id.Equals(PlanosLicencaRequest.Id));
                if (RecursoExistente is null) return BadRequest($"Plano de Licença não encontrada no banco de Dados!");


                // Atualizar os campos da entidade existente com os novos daods
                RecursoExistente.Id = PlanosLicencaRequest.Id;
                RecursoExistente.Nome = PlanosLicencaRequest.Nome;
                RecursoExistente.QuantidadeDeAcoes = PlanosLicencaRequest.QuantidadeDeAcoes;
                RecursoExistente.DataAtualizacao = DateTime.Now;

                // Chama o método DAL para atualizar a entidade no banco de dados
                await _dalPlanoLicenca.AtualizarAsync(RecursoExistente);

                // Retorna a entidade atualizada dentro de um Ok()
                return Ok(RecursoExistente);

            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar atualizar o recurso. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Remover(int id)
        {
            try
            {
                // Primeiro, recupera o recurso existente pelo ID
                PlanosLicencaModel? RecursoExistente = await _dalPlanoLicenca.RecuperarPorAsync(c => c.Id.Equals(id));

                // Retorna 404 Not Found se a entidade não existir
                if (RecursoExistente is null) return NotFound();

                try
                {
                    // Chama o método do DAL para remover o recurso
                    await _dalPlanoLicenca.DeletarAsync(RecursoExistente);

                    //Retorna 204 No Content se a remoção foi bem-sucedida
                    return NoContent();
                }
                catch (Exception ex)
                {
                    // Retorna um erro genérico ou detalhado se a remoção falhar
                    return StatusCode(500, $"Erro ao tentar remover o recurso. {ex.Message}");
                }
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar remover o recurso. {ex.Message}");
            }
        }

        //[HttpPut("AtualizarStatus/{id}")]
        //public async Task<ActionResult<PlanosLicencaModel>> AtualizarStatus([FromBody] AtualizarStatusPlanoLicenca PlanosLicencaRequest, int id)
        //{
        //    try
        //    {
        //        PlanosLicencaModel? RecursoExistente = await _dalPlanoLicenca.BuscarPorAsync(x => x.Id.Equals(id));

        //        // Retorna 404 Not Found se o recurso não existir
        //        if (RecursoExistente is null) return NotFound();

        //        RecursoExistente.Situacao = PlanosLicencaRequest.Situacao;
        //        await _dalPlanoLicenca.AtualizarAsync(RecursoExistente);

        //        // Retorna o recursso atualizado dentro de um Ok()
        //        return Ok(RecursoExistente);

        //    }
        //    catch (ValidationException ex)
        //    {
        //        // Retorna um erro de validação com o detalhe da mensagem
        //        return BadRequest($"Erro de validação: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Retorna um erro genérico com o detalhe da mensagem
        //        return StatusCode(500, $"Erro ao tentar atualizar o recurso. {ex.Message}");
        //    }
        //}
    } 
}