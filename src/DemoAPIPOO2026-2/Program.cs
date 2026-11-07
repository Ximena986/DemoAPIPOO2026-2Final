using System.Text;
using DemoAPIPOO2026_2.Autenticacion;
using DemoAPIPOO2026_2.Contexto;
using DemoAPIPOO2026_2.Estrategias;
using DemoAPIPOO2026_2.Repositorios;
using DemoAPIPOO2026_2.Servicios;
using DemoAPIPOO2026_2.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var jwt = builder.Configuration.GetSection("Jwt");
var clave = jwt["Key"] ?? throw new InvalidOperationException("No se configuró Jwt:Key. Configure la variable de entorno Jwt__Key.");
var emisor = jwt["Issuer"] ?? "InmobiliariaOriente";
var audiencia = jwt["Audience"] ?? "InmobiliariaOrienteApi";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API - Proyectos Inmobiliarios del Oriente Antioqueño",
        Version = "v1",
        Description = "API académica para registrar, consultar y consolidar proyectos inmobiliarios."
    });
    // El botón Authorize de Swagger ahora pide USUARIO y CONTRASEÑA (flujo OAuth2 "password"),
    // en lugar de pedir que se pegue manualmente un token.
    opciones.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Description = "Escriba manualmente su usuario y contraseña. Deje client_id y client_secret vacíos.",
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("/api/Autenticacion/token", UriKind.Relative),
                Scopes = new Dictionary<string, string>()
            }
        }
    });
    opciones.OperationFilter<SwaggerAuthorizeOperationFilter>();
});

builder.Services.AddSingleton<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ITokenServicio, TokenServicio>();
builder.Services.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = emisor,
            ValidateAudience = true,
            ValidAudience = audiencia,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSingleton<DatosInmobiliariosContexto>();
builder.Services.AddScoped<IProyectoRepositorio, ProyectoRepositorio>();
builder.Services.AddScoped<IConsolidacionFactory, ConsolidacionFactory>();
builder.Services.AddScoped<IProyectoServicio, ProyectoServicio>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Sirve la interfaz gráfica (wwwroot/index.html) con el formulario de usuario y contraseña.
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
