using DemoAPIPOO2026_2.Autenticacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DemoAPIPOO2026_2.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AutenticacionController : ControllerBase
{
    private readonly IServicioAutenticacion _autenticacion;
    public AutenticacionController(IServicioAutenticacion autenticacion) => _autenticacion = autenticacion;
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(RespuestaLoginDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginDTO credenciales)
    {
        var respuesta = _autenticacion.IniciarSesion(credenciales);
        if (respuesta == null) return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });
        return Ok(respuesta);
    }

    // Endpoint estándar OAuth2 (password) que usa el botón "Authorize" de Swagger para pedir usuario y contraseña.
    [AllowAnonymous]
    [HttpPost("token")]
    [Consumes("application/x-www-form-urlencoded")]
    public IActionResult Token([FromForm(Name = "username")] string? usuario, [FromForm(Name = "password")] string? contrasena)
    {
        var respuesta = _autenticacion.IniciarSesion(new LoginDTO { Usuario = usuario ?? string.Empty, Contrasena = contrasena ?? string.Empty });
        if (respuesta == null) return BadRequest(new { error = "invalid_grant", error_description = "Usuario o contraseña incorrectos." });
        return Ok(new
        {
            access_token = respuesta.Token,
            token_type = "Bearer",
            expires_in = (int)(respuesta.Expira - DateTime.UtcNow).TotalSeconds
        });
    }
}
