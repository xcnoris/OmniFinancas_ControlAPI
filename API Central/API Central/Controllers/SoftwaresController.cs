using API_Central.JWTServices;
using DataBase.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.EF;
using Modelos.EF.Revenda;
using Modelos.ModelosRequest.Licenca;
using Modelos.ModelosRequest.Software;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace API_Central.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = Roles.Admin)]
    public class SoftwaresController : ControllerBase
    {
        private readonly DAL<SoftwaresModel> _dalSoftwares;
        private readonly DAL<RevendaModel> _dalRevenda;
        public SoftwaresController(
            DAL<SoftwaresModel> dalSoftware,
            DAL<RevendaModel> dalRevenda
            )
        {
            _dalSoftwares = dalSoftware;
            _dalRevenda = dalRevenda;
        }
       
        [HttpPost]
        public async Task<ActionResult<SoftwaresModel>> CriarSoftware([FromBody] CriarSoftware SoftwareRequest)
        {
            try
            {
                RevendaModel? RevendaExistente = await _dalRevenda.RecuperarPorAsync(x => x.Id.Equals(SoftwareRequest.ProprietarioId));
                if (RevendaExistente is null) return BadRequest($"Revenda n�o encontrada no banco de Dados!");

                SoftwaresModel? SoftwareExistente = await _dalSoftwares.RecuperarPorAsync(x => x.Nome.Equals(SoftwareRequest.Nome));
                if (SoftwareExistente is not null) return BadRequest($"Nome de Software j� existe no banco de Dados!");

                SoftwaresModel NovoSoftware = new SoftwaresModel()
                {
                    Nome = SoftwareRequest.Nome,
                    Descri��o = SoftwareRequest.Descri��o,
                    Versao = SoftwareRequest.Versao,
                    ProprietarioId = SoftwareRequest.ProprietarioId,
                    Situacao = SoftwareRequest.Situacao,
                    DataCriacao = DateTime.Now,
                };


                // Adicionar o software a base de dados
                await _dalSoftwares.AdicionarAsync(NovoSoftware);

                // Retorna o objeto adicionado
                return NovoSoftware;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de valida��o com o detalhe da mensagem
                return BadRequest($"Erro de valida��o: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro gen�rico com a mensagem da exce��o
                return StatusCode(500, $"Erro ao tentar adicionar a entidade. {ex.Message}");
            }

        }

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<IEnumerable<SoftwaresModel>>> GetXmls()
        {
            try
            {
                IEnumerable<SoftwaresModel?> xmls = await _dalSoftwares.ListarAsync();
                return Ok(xmls);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro gen�rico com a mensagem da exce��o
                return StatusCode(500, $"Erro ao tentar buscar a entidade. {ex.Message}");
            }
        }


        [HttpGet("BuscarPorId/{id}")]
        public async Task<ActionResult<SoftwaresModel>> BuscarPorId(int id)
        {
            try
            {
                SoftwaresModel? SoftwaresExistente = await _dalSoftwares.BuscarPorAsync(c => c.Id.Equals(id));

                // Retorna 404 Not Found se a entidade n�o for compat�vel com a contabilidade
                if (SoftwaresExistente is null) return NotFound($"N�o foi encontrado nenhum software com este ID {id}.");

                return Ok(SoftwaresExistente);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro gen�rico com a mensagem da exce��o
                return StatusCode(500, $"Erro ao tentar buscar a entidade. {ex.Message}");
            }
        }

        [HttpGet("BuscarPorProprietario/{idProprietario}")]
        public async Task<ActionResult<IEnumerable<SoftwaresModel>>> GetPorCnpj(int idProprietario)
        {
            try
            {
                RevendaModel? Revenda = await _dalRevenda.BuscarPorAsync(c => c.Id.Equals(idProprietario));

                // Retorna 404 Not Found se a entidade n�o for compat�vel com a contabilidade
                if (Revenda is null) return NotFound($"N�o foi encontrado nenhuma revenda com este id {idProprietario}.");

                IEnumerable<SoftwaresModel>? SoftwaresList = await _dalSoftwares.RecuperarTodosPorAsync(x => x.ProprietarioId.Equals(idProprietario));

                if (SoftwaresList is null) return NotFound($"Essa revenda n�o possui nenhum software cadastrado");

                return Ok(SoftwaresList);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro gen�rico com a mensagem da exce��o
                return StatusCode(500, $"Erro ao tentar buscar a entidade. {ex.Message}");
            }
        }

        [HttpPut("AtualizarPorId/{id}")]
        public async Task<ActionResult<SoftwaresModel>> AtualizarPorId([FromBody] AtualizarSoftware SoftwareRequest, int id)
        {
            try
            {
                RevendaModel? RevendaExistente = await _dalRevenda.RecuperarPorAsync(x => x.Id.Equals(SoftwareRequest.ProprietarioId));
                if (RevendaExistente is null) return BadRequest($"Revenda n�o encontrada no banco de Dados!");
               
                // Primeira, recupera a entidade pelo ID
                var SoftwareExistente = await _dalSoftwares.RecuperarPorAsync(x => x.Equals(id));
                // Retorna 404 Not Found se a entidade n�o existir
                if (SoftwareExistente is null) return NotFound($"Id {id} n�o existe no banco de dados!");

                // Atualizar os campos da entidade existente com os novos daods
                SoftwareExistente.Id = SoftwareRequest.Id;
                SoftwareExistente.Nome = SoftwareRequest.Nome;
                SoftwareExistente.Descri��o = SoftwareRequest.Descri��o;
                SoftwareExistente.Versao = SoftwareRequest.Versao;
                SoftwareExistente.ProprietarioId = SoftwareRequest.ProprietarioId;
                SoftwareExistente.Situacao = SoftwareRequest.Situacao;
                SoftwareExistente.DataAtualizacao = DateTime.Now;

                // Chama o m�todo DAL para atualizar a entidade no banco de dados
                await _dalSoftwares.AtualizarAsync(SoftwareExistente);

                // Retorna a entidade atualizada dentro de um Ok()
                return Ok(SoftwareExistente);
         
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de valida��o com o detalhe da mensagem
                return BadRequest($"Erro de valida��o: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro gen�rico com a mensagem da exce��o
                return StatusCode(500, $"Erro ao tentar atualizar o recurso. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Remover(int id)
        {
            try
            {
                // Primeiro, recupera o recurso existente pelo ID
                SoftwaresModel? RecursoExistente = await _dalSoftwares.RecuperarPorAsync(c => c.Id.Equals(id));

                // Retorna 404 Not Found se a entidade n�o existir
                if (RecursoExistente is null) return NotFound();

                try
                {
                    // Chama o m�todo do DAL para remover o recurso
                    await _dalSoftwares.DeletarAsync(RecursoExistente);

                    //Retorna 204 No Content se a remo��o foi bem-sucedida
                    return NoContent();
                }
                catch (Exception ex)
                {
                    // Retorna um erro gen�rico ou detalhado se a remo��o falhar
                    return StatusCode(500, $"Erro ao tentar remover o recurso. {ex.Message}");
                }
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de valida��o com o detalhe da mensagem
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro gen�rico com a mensagem da exce��o
                return StatusCode(500, $"Erro ao tentar remover o recurso. {ex.Message}");
            }
        }

        [HttpPut("AtualizarStatus/{id}")]
        public async Task<ActionResult<SoftwaresModel>> AtualizarStatus([FromBody] AtualizarStatusPlanoLicenca softwareRequest, int id)
        {
            try
            {
                SoftwaresModel? SoftwareExistente = await _dalSoftwares.BuscarPorAsync(x => x.Id.Equals(id));

                // Retorna 404 Not Found se o recurso n�o existir
                if (SoftwareExistente is null) return NotFound();

                SoftwareExistente.Situacao = softwareRequest.Situacao;
                await _dalSoftwares.AtualizarAsync(SoftwareExistente);

                // Retorna o recursso atualizado dentro de um Ok()
                return Ok(SoftwareExistente);
             
            }
            catch (ValidationException ex)
            {
                // Retorna um erro de valida��o com o detalhe da mensagem
                return BadRequest($"Erro de valida��o: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Retorna um erro gen�rico com o detalhe da mensagem
                return StatusCode(500, $"Erro ao tentar atualizar a entidade. {ex.Message}");
            }
        }
    }
}
