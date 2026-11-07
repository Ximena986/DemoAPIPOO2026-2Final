using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace DemoAPIPOO2026_2.Autenticacion;
public class TokenServicio : ITokenServicio
{
    private readonly IConfiguration _configuracion;
    public TokenServicio(IConfiguration configuracion) => _configuracion = configuracion;
    public (string Token, DateTime Expira) CrearToken(UsuarioAplicacion usuario)
    {
        var jwt = _configuracion.GetSection("Jwt");
        var claveTexto = jwt["Key"] ?? throw new InvalidOperationException("No se configuró Jwt:Key.");
        var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveTexto));
        var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);
        var expira = DateTime.UtcNow.AddHours(2);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Usuario),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };
        var token = new JwtSecurityToken(jwt["Issuer"], jwt["Audience"], claims,
            expires: expira, signingCredentials: credenciales);
        return (new JwtSecurityTokenHandler().WriteToken(token), expira);
    }
}
