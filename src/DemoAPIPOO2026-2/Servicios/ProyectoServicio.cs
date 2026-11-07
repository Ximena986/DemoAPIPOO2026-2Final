using DemoAPIPOO2026_2.DTOs;
using DemoAPIPOO2026_2.Estrategias;
using DemoAPIPOO2026_2.Modelos;
using DemoAPIPOO2026_2.Repositorios;
namespace DemoAPIPOO2026_2.Servicios;
public class ProyectoServicio : IProyectoServicio
{
    private readonly IProyectoRepositorio _repositorio;
    private readonly IConsolidacionFactory _factory;
    private static readonly string[] Municipios =
    {
        "Rionegro", "La Ceja", "El Carmen de Viboral", "Marinilla",
        "Guarne", "El Retiro", "San Vicente", "El Santuario"
    };
    public ProyectoServicio(IProyectoRepositorio repositorio, IConsolidacionFactory factory)
    {
        _repositorio = repositorio;
        _factory = factory;
    }
    public List<ProyectoInmobiliario> ObtenerTodos(string? municipio, string? tipo)
    {
        var proyectos = _repositorio.ObtenerTodos();
        if (!string.IsNullOrWhiteSpace(municipio))
            proyectos = proyectos.Where(p => p.Municipio.Equals(municipio.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
        if (!string.IsNullOrWhiteSpace(tipo))
            proyectos = proyectos.Where(p => p.Tipo.Equals(tipo.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
        return proyectos;
    }
    public List<ProyectoInmobiliario> ObtenerConsolidados() =>
        _repositorio.ObtenerTodos().Where(p => p.EstadoConsolidacion.Equals("Consolidado", StringComparison.OrdinalIgnoreCase)).ToList();
    public ProyectoInmobiliario? ObtenerPorId(int id) => _repositorio.ObtenerPorId(id);
    public ProyectoInmobiliario Crear(ProyectoDTO dto)
    {
        ValidarDatos(dto);
        var proyecto = new ProyectoInmobiliario
        {
            Nombre = dto.Nombre.Trim(), Promotor = dto.Promotor.Trim(),
            Municipio = ObtenerMunicipioOficial(dto.Municipio), Tipo = NormalizarTipo(dto.Tipo),
            AreaMetrosCuadrados = dto.AreaMetrosCuadrados, ValorEstimado = dto.ValorEstimado,
            TieneLicencia = dto.TieneLicencia, TieneConceptoUsoSuelo = dto.TieneConceptoUsoSuelo
        };
        proyecto.EstadoConsolidacion = _factory.Crear(proyecto.Tipo).Evaluar(proyecto);
        return _repositorio.Crear(proyecto);
    }
    public ProyectoInmobiliario? Actualizar(int id, ProyectoDTO dto)
    {
        if (_repositorio.ObtenerPorId(id) == null) return null;
        ValidarDatos(dto);
        var proyecto = new ProyectoInmobiliario
        {
            Id = id, Nombre = dto.Nombre.Trim(), Promotor = dto.Promotor.Trim(),
            Municipio = ObtenerMunicipioOficial(dto.Municipio), Tipo = NormalizarTipo(dto.Tipo),
            AreaMetrosCuadrados = dto.AreaMetrosCuadrados, ValorEstimado = dto.ValorEstimado,
            TieneLicencia = dto.TieneLicencia, TieneConceptoUsoSuelo = dto.TieneConceptoUsoSuelo
        };
        proyecto.EstadoConsolidacion = _factory.Crear(proyecto.Tipo).Evaluar(proyecto);
        return _repositorio.Actualizar(proyecto) ? proyecto : null;
    }
    public bool Eliminar(int id) => _repositorio.Eliminar(id);
    private static void ValidarDatos(ProyectoDTO dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Trim().Length < 3)
            throw new ArgumentException("El nombre es obligatorio y debe tener al menos 3 caracteres.");
        if (string.IsNullOrWhiteSpace(dto.Promotor)) throw new ArgumentException("El promotor es obligatorio.");
        ObtenerMunicipioOficial(dto.Municipio);
        NormalizarTipo(dto.Tipo);
        if (dto.AreaMetrosCuadrados <= 0) throw new ArgumentException("El área debe ser mayor que cero.");
        if (dto.ValorEstimado <= 0) throw new ArgumentException("El valor estimado debe ser mayor que cero.");
    }
    private static string ObtenerMunicipioOficial(string municipio)
    {
        if (string.IsNullOrWhiteSpace(municipio)) throw new ArgumentException("El municipio es obligatorio.");
        var encontrado = Municipios.FirstOrDefault(m => m.Equals(municipio.Trim(), StringComparison.OrdinalIgnoreCase));
        return encontrado ?? throw new ArgumentException("Municipio no permitido. Seleccione uno de los ocho municipios habilitados.");
    }
    private static string NormalizarTipo(string tipo)
    {
        if (string.IsNullOrWhiteSpace(tipo)) throw new ArgumentException("El tipo de proyecto es obligatorio.");
        if (tipo.Trim().Equals("Urbano", StringComparison.OrdinalIgnoreCase)) return "Urbano";
        if (tipo.Trim().Equals("Rural", StringComparison.OrdinalIgnoreCase)) return "Rural";
        throw new ArgumentException("El tipo de proyecto debe ser Urbano o Rural.");
    }
}
