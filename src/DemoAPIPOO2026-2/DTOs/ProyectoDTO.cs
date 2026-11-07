using System.ComponentModel.DataAnnotations;

namespace DemoAPIPOO2026_2.DTOs;

public class ProyectoDTO
{
    [Required, StringLength(120, MinimumLength = 3)]
    public string Nombre { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 2)]
    public string Promotor { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string Municipio { get; set; } = string.Empty;

    [Required, RegularExpression("^(Urbano|Rural)$", ErrorMessage = "El tipo debe ser Urbano o Rural.")]
    public string Tipo { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "10000000")]
    public decimal AreaMetrosCuadrados { get; set; }

    [Range(typeof(decimal), "0.01", "100000000000")]
    public decimal ValorEstimado { get; set; }

    public bool TieneLicencia { get; set; }
    public bool TieneConceptoUsoSuelo { get; set; }
}
