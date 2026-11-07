using Microsoft.AspNetCore.Identity;
namespace DemoAPIPOO2026_2.Autenticacion;
public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly List<UsuarioAplicacion> _usuarios;
    public UsuarioRepositorio()
    {
        var hasher = new PasswordHasher<UsuarioAplicacion>();
        var administrador = new UsuarioAplicacion { Usuario = "admin", Rol = "Administrador" };
        administrador.HashContrasena = hasher.HashPassword(administrador, "Admin123*");
        var consulta = new UsuarioAplicacion { Usuario = "consulta", Rol = "Consulta" };
        consulta.HashContrasena = hasher.HashPassword(consulta, "Consulta123*");
        _usuarios = new List<UsuarioAplicacion> { administrador, consulta };
    }
    public UsuarioAplicacion? ObtenerPorUsuario(string usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario)) return null;
        return _usuarios.FirstOrDefault(u => u.Usuario.Equals(usuario.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}
