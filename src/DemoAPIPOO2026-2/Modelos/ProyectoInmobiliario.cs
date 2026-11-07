namespace DemoAPIPOO2026_2.Modelos;

public class ProyectoInmobiliario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Promotor { get; set; } = string.Empty;
    public string Municipio { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal AreaMetrosCuadrados { get; set; }
    public decimal ValorEstimado { get; set; }
    public bool TieneLicencia { get; set; }
    public bool TieneConceptoUsoSuelo { get; set; }
    public string EstadoConsolidacion { get; set; } = string.Empty;
}
