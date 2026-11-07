using DemoAPIPOO2026_2.Autenticacion;
using Xunit;
namespace DemoAPIPOO2026_2.Tests;
public class AutenticacionTests
{
    private sealed class TokenFalso : ITokenServicio
    {
        public (string Token, DateTime Expira) CrearToken(UsuarioAplicacion usuario) => ("token-de-prueba", DateTime.UtcNow.AddHours(2));
    }
    [Fact]
    public void UsuarioAdminConClaveCorrectaObtieneRespuesta()
    {
        var servicio = new ServicioAutenticacion(new UsuarioRepositorio(), new TokenFalso());
        var respuesta = servicio.IniciarSesion(new LoginDTO { Usuario = "admin", Contrasena = "Admin123*" });
        Assert.NotNull(respuesta); Assert.Equal("Administrador", respuesta!.Rol); Assert.Equal("token-de-prueba", respuesta.Token);
    }
    [Fact]
    public void ClaveIncorrectaNoObtieneRespuesta()
    {
        var servicio = new ServicioAutenticacion(new UsuarioRepositorio(), new TokenFalso());
        Assert.Null(servicio.IniciarSesion(new LoginDTO { Usuario = "admin", Contrasena = "incorrecta" }));
    }
    [Fact]
    public void UsuarioInexistenteNoObtieneRespuesta()
    {
        var servicio = new ServicioAutenticacion(new UsuarioRepositorio(), new TokenFalso());
        Assert.Null(servicio.IniciarSesion(new LoginDTO { Usuario = "desconocido", Contrasena = "x" }));
    }
}
