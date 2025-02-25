using DataBase.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modelos.EF.Login;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adiciona o serviço ao contêiner de injeção de dependência.
// Configura o DbContext 'Context' para utilizar o SQL Server como banco de dados,
// especificando a string de conexão 'DefaultConnection' arquivo de configuração (appsettings.json).
builder.Services.AddDbContext<MyServiceStoreDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Outras configurações
builder.Services.Configure<IdentityOptions>(options =>
{
    // Configurações adicionais para o Identity
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
    options.User.RequireUniqueEmail = true; // Exemplo: requer que o e-mail seja único
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(DAL<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

// Outras configurações e ma

app.UseRouting(); // Adiciona suporte ao roteamento
app.UseAuthentication(); // Adiciona middleware de autenticação
app.UseAuthorization(); // Adiciona middleware de autorização

app.MapControllers();

app.Run();
