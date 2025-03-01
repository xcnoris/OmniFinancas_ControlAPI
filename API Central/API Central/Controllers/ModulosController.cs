using DataBase.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF;
using Modelos.EF.Revenda;
using Modelos.ModelosRequest.Licenca;
using Modelos.ModelosRequest.Modulos;
using Modelos.ModelosRequest.Software;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace API_Central.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulosController : ControllerBase
    {
        private readonly DAL<SoftwaresModel> _dalSoftwares;
        private readonly DAL<ModulosModel> _dalModulo;
        public ModulosController(
            DAL<SoftwaresModel> dalSoftware,
            DAL<ModulosModel> dalModulo
            )
        {
            _dalSoftwares = dalSoftware;
            _dalModulo = dalModulo;
        }
       
        [HttpPost]
        public async Task<ActionResult<ModulosModel>> CriarSoftware([FromBody] CriarModulosSoftware RecursoRequest)
        {
            try
            {
                SoftwaresModel? RecursoExist = await _dalSoftwares.RecuperarPorAsync(x => x.Id.Equals(RecursoRequest.SoftwareId));
                if (RecursoExist is null) return BadRequest($"Software não encontrada no banco de Dados!");


                ModulosModel NovoRecurso = new ModulosModel()
                {
                    Nome = RecursoRequest.Nome,
                    Descricao = RecursoRequest.Descricao,
                    SoftwareId = RecursoRequest.SoftwareId,
                    Situacao = RecursoRequest.Situacao,
                    DataCriacao = DateTime.Now,
                };


                // Adicionar o software a base de dados
                await _dalModulo.AdicionarAsync(NovoRecurso);

                // Retorna o objeto adicionado
                return NovoRecurso;
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
                return StatusCode(500, $"Erro ao tentar adicionar o recurso. {ex.Message}");
            }

        }

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<IEnumerable<ModulosModel>>> GetXmls()
        {
            try
            {
                IEnumerable<ModulosModel?> xmls = await _dalModulo.ListarAsync();
                return Ok(xmls);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar buscar o recurso. {ex.Message}");
            }
        }


        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<ModulosModel>> BuscarPorId(int id)
        {
            try
            {
                ModulosModel? RecursoExistente = await _dalModulo.BuscarPorAsync(c => c.Id.Equals(id));

                // Retorna 404 Not Found se a entidade não for compatível com a contabilidade
                if (RecursoExistente is null) return NotFound($"Não foi encontrado nenhum Modulo com este ID {id}.");

                return Ok(RecursoExistente);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar buscar o recurso. {ex.Message}");
            }
        }

        [HttpGet("BuscarPorSoftware/{IdSoftware}")]
        public async Task<ActionResult<IEnumerable<ModulosModel>>> GetPorCnpj(int IdSoftware)
        {
            try
            {
                SoftwaresModel? SoftwareExiste = await _dalSoftwares.BuscarPorAsync(c => c.Id.Equals(IdSoftware));

                // Retorna 404 Not Found se a entidade não for compatível com a contabilidade
                if (SoftwareExiste is null) return NotFound($"Não foi encontrado nenhum software com este id {IdSoftware}.");

                IEnumerable<ModulosModel>? RecursoList = await _dalModulo.RecuperarTodosPorAsync(x => x.SoftwareId.Equals(IdSoftware));

                if (RecursoList is null) return NotFound($"Esse Software não possui nenhum modulo cadastrado!");

                return Ok(RecursoList);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar buscar o recurso. {ex.Message}");
            }
        }

        [HttpPut("AtualizarPorId/{id}")]
        public async Task<ActionResult<ModulosModel>> AtualizarPorId([FromBody] AtualizarModulo RecursoRequest, int id)
        {
            try
            {
                ModulosModel? RecursoExist = await _dalModulo.RecuperarPorAsync(x => x.Id.Equals(id));
                if (RecursoExist is null) return BadRequest($"Modulo não encontrada no banco de Dados!");

                SoftwaresModel? SoftwareExist = await _dalSoftwares.RecuperarPorAsync(x => x.Id.Equals(RecursoRequest.SoftwareId));
                if (RecursoExist is null) return BadRequest($"Software não encontrada no banco de Dados!");

                // Primeira, recupera a entidade pelo ID
                ModulosModel RecursoExistente = await _dalModulo.RecuperarPorAsync(x => x.Equals(id));
         

                // Atualizar os campos da entidade existente com os novos dados
                RecursoExistente.Nome = RecursoRequest.Nome;
                RecursoExistente.Descricao = RecursoRequest.Descricao;
                RecursoExistente.SoftwareId = RecursoRequest.SoftwareId;
                RecursoExistente.Situacao = RecursoRequest.Situacao;
                RecursoExistente.DataAtualizacao = DateTime.Now;

                // Chama o método DAL para atualizar a entidade no banco de dados
                await _dalModulo.AtualizarAsync(RecursoExistente);

                // Retorna a entidade atualizada dentro de um Ok()
                return Ok(RecursoExistente);
         
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar atualizar o recurso. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Remover(int id)
        {
            try
            {
                // Primeiro, recupera o recurso existente pelo ID
                ModulosModel? RecursoExistente = await _dalModulo.RecuperarPorAsync(c => c.Id.Equals(id));

                // Retorna 404 Not Found se a entidade não existir
                if (RecursoExistente is null) return NotFound();

                try
                {
                    // Chama o método do DAL para remover o recurso
                    await _dalModulo.DeletarAsync(RecursoExistente);

                    //Retorna 204 No Content se a remoção foi bem-sucedida
                    return NoContent();
                }
                catch (Exception ex)
                {
                    // Retorna um erro genérico ou detalhado se a remoção falhar
                    return StatusCode(500, $"Erro ao tentar remover o recurso. {ex.Message}");
                }
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar remover o recurso. {ex.Message}");
            }
        }

        [HttpPut("AtualizarStatus/{id}")]
        public async Task<ActionResult<ModulosModel>> AtualizarStatus([FromBody] AtualizarStatusModulo RecursoRequest, int id)
        {
            try
            {
                ModulosModel? RecursosExistente = await _dalModulo.BuscarPorAsync(x => x.Id.Equals(id));

                // Retorna 404 Not Found se o recurso não existir
                if (RecursosExistente is null) return NotFound();

                RecursosExistente.Situacao = RecursoRequest.Situacao;
                await _dalModulo.AtualizarAsync(RecursosExistente);

                // Retorna o recursso atualizado dentro de um Ok()
                return Ok(RecursosExistente);
             
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de validação com o detalhe da mensagem
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com o detalhe da mensagem
                return StatusCode(500, $"Erro ao tentar atualizar o recurso. {ex.Message}");
            }
        }
    }
}
