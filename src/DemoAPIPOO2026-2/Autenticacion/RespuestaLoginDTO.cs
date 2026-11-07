namespace DemoAPIPOO2026_2.Autenticacion;
public class RespuestaLoginDTO
{
    public string Token { get; set; } = string.Empty;
    public string Tipo { get; set; } = "Bearer";
    public DateTime Expira { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
}
