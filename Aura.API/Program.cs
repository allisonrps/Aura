using Aura.Infrastructure.Data;
using Aura.Application.Interfaces;
using Aura.Application.Validators;
using Aura.Infrastructure.Services;
using Aura.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens; 
using System.Text; 
using DotNetEnv;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURAÇÃO DE AMBIENTE ---
DotNetEnv.Env.TraversePath().Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

// Validação imediata: Se a chave for nula, a aplicação para aqui com uma mensagem clara.
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("Aura Error: Variável JWT_KEY não encontrada no arquivo .env.");
}

// --- 2. SERVIÇOS DO CONTAINER (DI) ---

builder.Services.AddDbContext<AuraDbContext>(options =>
{
    if (string.IsNullOrEmpty(connectionString))
        throw new InvalidOperationException("Aura Error: Variável DB_CONNECTION_STRING não encontrada.");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<StudentRepository>();


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();


// Configuração de Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        // Usamos a variável já validada acima
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Registro de Dependências
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>(); 
builder.Services.AddScoped<TeacherRepository>();

builder.Services.AddControllers(); 
builder.Services.AddOpenApi();

var app = builder.Build();

// --- 3. PIPELINE DE REQUISIÇÕES ---

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.WithTitle("Aura API - Gestão de Aulas").WithTheme(ScalarTheme.Moon);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();