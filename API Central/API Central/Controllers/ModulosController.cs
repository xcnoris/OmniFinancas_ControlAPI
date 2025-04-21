using DataBase.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs.Modulos.InPut;
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
        private readonly DAL<ModulosModel> _dalModulo;
        private readonly DAL<SoftwaresModel> _dalSoftwares;
        public ModulosController(
            DAL<SoftwaresModel> dalSoftware,
            DAL<ModulosModel> dalModulo
            )
        {
            _dalModulo = dalModulo;
            _dalSoftwares = dalSoftware;
        }
       
        [HttpPost]
        public async Task<ActionResult<ModulosModel>> Criar([FromBody] CriarModulo ModuloRequest)
        {
            try
            {
                SoftwaresModel? SoftwareExistente = await _dalSoftwares.RecuperarPorAsync(x => x.Id.Equals(ModuloRequest.SoftwareId));
                if (SoftwareExistente is null) return BadRequest($"Software não encontrado no banco de Dados!");

                ModulosModel NovoSoftware = new ModulosModel()
                {
                    Identificacao = ModuloRequest.Identificao,
                    Nome = ModuloRequest.Nome,
                    Descricao = ModuloRequest.Descricao,
                    SoftwareId = ModuloRequest.SoftwareId,
                    Valor = ModuloRequest.Valor,
                    Situacao = ModuloRequest.Situacao,
                    DataCriacao = DateTime.Now,
                };


                // Adicionar o software a base de dados
                await _dalModulo.AdicionarAsync(NovoSoftware);
                ModulosModel NovoModulo = await _dalModulo.RecuperarPorAsync(x => x.Identificacao.Equals(ModuloRequest.Identificao));
                // Retorna o objeto adicionado
                return NovoModulo;
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

        [HttpGet]
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
                return StatusCode(500, $"Erro ao tentar buscar a entidade. {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ModulosModel>> BuscarPorId(int id)
        {
            try
            {
                ModulosModel? ModuloExistente = await _dalModulo.BuscarPorAsync(c => c.Id.Equals(id));

                // Retorna 404 Not Found se a entidade não for compatível com a contabilidade
                if (ModuloExistente is null) return NotFound($"Não foi encontrado nenhum Modulo com este ID {id}.");

                return Ok(ModuloExistente);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar buscar a entidade. {ex.Message}");
            }
        }

        [HttpGet("BuscarPorSoftware/{idSoftware}")]
        public async Task<ActionResult<IEnumerable<RetornoModulo>>> GetPorCnpj(int idSoftware)
        {
            try
            {
                SoftwaresModel? Software = await _dalSoftwares.BuscarPorAsync(c => c.Id.Equals(idSoftware));

                // Retorna 404 Not Found se a entidade não for compatível com a contabilidade
                if (Software is null) return NotFound($"Não foi encontrado nenhum software com este id {idSoftware}.");

                IEnumerable<ModulosModel>? ModuloList = await _dalModulo.RecuperarTodosPorAsync(x => x.SoftwareId.Equals(idSoftware));

                if (ModuloList is null || !ModuloList.Any())
                    return NotFound("Esse software não possui nenhum módulo cadastrado.");


                var retornoModulos = ModuloList.Select(modulo => new RetornoModulo
                {
                    Id = modulo.Id,
                    Identificacao = modulo.Identificacao,
                    Nome = modulo.Nome,
                    Descricao = modulo.Descricao,
                    SoftwareId = modulo.SoftwareId,
                    Valor = modulo.Valor,
                    Situacao = modulo.Situacao
                }).ToList();

                return Ok(retornoModulos);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro genérico com a mensagem da exceção
                return StatusCode(500, $"Erro ao tentar buscar a entidade. {ex.Message}");
            }
        }

        [HttpPut("Atualizar/{id}")]
        public async Task<ActionResult<string>> AtualizarPorId([FromBody] AtualizarModulo ModuloRequest, int id)
        {
            try
            {
                SoftwaresModel? SoftwareExistente = await _dalSoftwares.RecuperarPorAsync(x => x.Id.Equals(ModuloRequest.SoftwareId));
                if (SoftwareExistente is null) return BadRequest($"Software não encontrado no banco de Dados!");

               
                // Primeira, recupera a entidade pelo ID
                ModulosModel? ModuloExistente = await _dalModulo.RecuperarPorAsync(x => x.Id.Equals(id));
                // Retorna 404 Not Found se a entidade não existir
                if (ModuloExistente is null) return NotFound($"Id {id} não existe no banco de dados!");

                // Atualizar os campos da entidade existente com os novos daods
                ModuloExistente.Id = ModuloRequest.Id;
                ModuloExistente.Identificacao = ModuloRequest.Identificacao;
                ModuloExistente.Nome = ModuloRequest.Nome;
                ModuloExistente.Descricao = ModuloRequest.Descricao;
                ModuloExistente.SoftwareId = ModuloRequest.SoftwareId;
                ModuloExistente.Valor = ModuloRequest.Valor;
                ModuloExistente.Situacao = ModuloRequest.Situacao;
                ModuloExistente.DataAtualizacao = DateTime.Now;

                // Chama o método DAL para atualizar a entidade no banco de dados
                await _dalModulo.AtualizarAsync(ModuloExistente);

                // Retorna a entidade atualizada dentro de um Ok()
                return Ok("Modulo Atualizado com sucesso!");
         
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
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                // Recupera o módulo pelo ID
                ModulosModel? recursoExistente = await _dalModulo.RecuperarPorAsync(c => c.Id.Equals(id));

                if (recursoExistente is null)
                    return NotFound($"Nenhum módulo encontrado com o ID {id}.");

                // Remove o recurso
                await _dalModulo.DeletarAsync(recursoExistente);

                // Retorna 204 No Content
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao tentar remover o recurso. {ex.Message}");
            }
        }


        //[HttpPut("AtualizarStatus/{id}")]
        //public async Task<ActionResult<SoftwaresModel>> AtualizarStatus([FromBody] AtualizarStatusPlanoLicenca softwareRequest, int id)
        //{
        //    try
        //    {
        //        SoftwaresModel? SoftwareExistente = await _dalModulo.BuscarPorAsync(x => x.Id.Equals(id));

        //        // Retorna 404 Not Found se o recurso não existir
        //        if (SoftwareExistente is null) return NotFound();

        //        SoftwareExistente.Situacao = softwareRequest.Situacao;
        //        await _dalModulo.AtualizarAsync(SoftwareExistente);

        //        // Retorna o recursso atualizado dentro de um Ok()
        //        return Ok(SoftwareExistente);
             
        //    }
        //    catch (ValidationException ex)
        //    {
        //        // Retorna um erro de validação com o detalhe da mensagem
        //        return BadRequest($"Erro de validação: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Retorna um erro genérico com o detalhe da mensagem
        //        return StatusCode(500, $"Erro ao tentar atualizar a entidade. {ex.Message}");
        //    }
        //}
    }
}
