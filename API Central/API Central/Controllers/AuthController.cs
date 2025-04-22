using System.Threading.Tasks;
using API_Central.Services;
using DataBase.Data;
using MetodosGerais;
using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs.PlanoLicenca;
using Modelos.EF.Contrato;
using Modelos.EF.Login;
using Modelos.EF.Revenda;
using Modelos.Enuns;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly DAL<UserLoginModel> _dalUserLogin;
    private readonly DAL<UsuariosRevendaModel> _dalUserRevenda;

    public AuthController(
        JwtService jwtService,
        DAL<UserLoginModel> dalUserLogin,
        DAL<UsuariosRevendaModel> dalUserRevenda
        )
    {
        _jwtService = jwtService;
        _dalUserLogin = dalUserLogin;
        _dalUserRevenda = dalUserRevenda;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Exemplo simples: verificação fixa de usuário/senha
        UserLoginModel? UserExistente = await _dalUserLogin.RecuperarPorAsync(user => user.Email == model.NomeUser);

        if (UserExistente == null) return NotFound("Nenhum Usuario Encontrado com esse Nome!");
        if (UserExistente.HashSenha != HashStringService.GerarHash256(model.Senha))
            return BadRequest("Senha incorreta!");

        List<string> ListaClain = new List<string>();
        ListaClain.Add("Admin");

        var token = _jwtService.GenerateToken(UserExistente, ListaClain);
        return Ok(new { token });
        

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        UsuariosRevendaModel? userRevenda = await _dalUserRevenda.RecuperarPorAsync(x => x.Id == model.UsuarioRevendaId);
        if (userRevenda == null) return NotFound($"Não foi encontrado nenhum usuario de revenda com esse ID: {model.UsuarioRevendaId}");

        var newuser = new UserLoginModel
        {
            Nome = model.NomeUser,
            Email = model.email,
            HashSenha = HashStringService.GerarHash256(model.Senha),
            UsuarioRevendaId = model.UsuarioRevendaId,
            Situacao = Situacao.Ativo,
            DataCriacao = DateTime.Now,
            DataAtualizacao = DateTime.Now
        };

        try
        {
            await _dalUserLogin.AdicionarAsync(newuser);

            return Ok("Usuário registrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
      
           

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserLoginModel>>> ListarTodos()
    {
        try
        {
            var lista = await _dalUserLogin.ListarAsync();
            return Ok(lista);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao listar vínculos. {ex.Message}");
        }
    }
}
