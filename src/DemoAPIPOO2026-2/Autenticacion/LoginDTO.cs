using System.ComponentModel.DataAnnotations;
namespace DemoAPIPOO2026_2.Autenticacion;
public class LoginDTO
{
    [Required] public string Usuario { get; set; } = string.Empty;
    [Required] public string Contrasena { get; set; } = string.Empty;
}
