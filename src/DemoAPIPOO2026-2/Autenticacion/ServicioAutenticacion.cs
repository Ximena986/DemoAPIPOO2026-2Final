using Microsoft.AspNetCore.Identity;
namespace DemoAPIPOO2026_2.Autenticacion;
public class ServicioAutenticacion : IServicioAutenticacion
{
    private readonly IUsuarioRepositorio _usuarios;
    private readonly ITokenServicio _tokens;
    private readonly PasswordHasher<UsuarioAplicacion> _hasher = new();
    public ServicioAutenticacion(IUsuarioRepositorio usuarios, ITokenServicio tokens)
    { _usuarios = usuarios; _tokens = tokens; }
    public RespuestaLoginDTO? IniciarSesion(LoginDTO credenciales)
    {
        if (credenciales == null || string.IsNullOrWhiteSpace(credenciales.Usuario) || string.IsNullOrEmpty(credenciales.Contrasena)) return null;
        var usuario = _usuarios.ObtenerPorUsuario(credenciales.Usuario);
        if (usuario == null) return null;
        var resultado = _hasher.VerifyHashedPassword(usuario, usuario.HashContrasena, credenciales.Contrasena);
        if (resultado == PasswordVerificationResult.Failed) return null;
        var (token, expira) = _tokens.CrearToken(usuario);
        return new RespuestaLoginDTO { Token = token, Expira = expira, Usuario = usuario.Usuario, Rol = usuario.Rol };
    }
}
