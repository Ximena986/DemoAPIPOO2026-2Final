using DemoAPIPOO2026_2.Modelos;

namespace DemoAPIPOO2026_2.Contexto;

// Almacenamiento temporal en memoria para la demostración académica.
public class DatosInmobiliariosContexto
{
    private readonly List<ProyectoInmobiliario> _proyectos;
    private int _siguienteId;
    private readonly object _candado = new();

    public DatosInmobiliariosContexto()
    {
        _proyectos = new List<ProyectoInmobiliario>
        {
            new() { Id = 1, Nombre = "Reserva del Río", Promotor = "Desarrollos Oriente S.A.S.", Municipio = "Rionegro", Tipo = "Urbano", AreaMetrosCuadrados = 12500, ValorEstimado = 8500000000, TieneLicencia = true, TieneConceptoUsoSuelo = true, EstadoConsolidacion = "Consolidado" },
            new() { Id = 2, Nombre = "Parcelación El Yarumo", Promotor = "Inversiones La Ceja", Municipio = "La Ceja", Tipo = "Rural", AreaMetrosCuadrados = 42000, ValorEstimado = 3900000000, TieneLicencia = false, TieneConceptoUsoSuelo = true, EstadoConsolidacion = "Consolidado" },
            new() { Id = 3, Nombre = "Mirador de la Palma", Promotor = "Constructora Altavista", Municipio = "El Carmen de Viboral", Tipo = "Urbano", AreaMetrosCuadrados = 8700, ValorEstimado = 6100000000, TieneLicencia = true, TieneConceptoUsoSuelo = true, EstadoConsolidacion = "Consolidado" },
            new() { Id = 4, Nombre = "Finca Raíz San Isidro", Promotor = "Proyectos del Bosque", Municipio = "Marinilla", Tipo = "Rural", AreaMetrosCuadrados = 58000, ValorEstimado = 2700000000, TieneLicencia = false, TieneConceptoUsoSuelo = false, EstadoConsolidacion = "Pendiente de concepto de uso del suelo" },
            new() { Id = 5, Nombre = "Parque Empresarial Guarne", Promotor = "Oriente Logístico", Municipio = "Guarne", Tipo = "Urbano", AreaMetrosCuadrados = 22000, ValorEstimado = 12000000000, TieneLicencia = true, TieneConceptoUsoSuelo = true, EstadoConsolidacion = "Consolidado" },
            new() { Id = 6, Nombre = "Lotes La Cristalina", Promotor = "Hábitat Verde", Municipio = "El Retiro", Tipo = "Rural", AreaMetrosCuadrados = 31500, ValorEstimado = 4600000000, TieneLicencia = false, TieneConceptoUsoSuelo = true, EstadoConsolidacion = "Consolidado" },
            new() { Id = 7, Nombre = "Viviendas San Vicente", Promotor = "Urbanizar Oriente", Municipio = "San Vicente", Tipo = "Urbano", AreaMetrosCuadrados = 9600, ValorEstimado = 5200000000, TieneLicencia = true, TieneConceptoUsoSuelo = true, EstadoConsolidacion = "Consolidado" },
            new() { Id = 8, Nombre = "Parcelación La Esperanza", Promotor = "Tierras del Santuario", Municipio = "El Santuario", Tipo = "Rural", AreaMetrosCuadrados = 67000, ValorEstimado = 3300000000, TieneLicencia = false, TieneConceptoUsoSuelo = false, EstadoConsolidacion = "Pendiente de concepto de uso del suelo" }
        };
        _siguienteId = _proyectos.Max(p => p.Id) + 1;
    }

    public List<ProyectoInmobiliario> ObtenerTodos()
    {
        lock (_candado) return _proyectos.Select(Copiar).ToList();
    }

    public ProyectoInmobiliario? BuscarPorId(int id)
    {
        lock (_candado)
        {
            var proyecto = _proyectos.FirstOrDefault(p => p.Id == id);
            return proyecto == null ? null : Copiar(proyecto);
        }
    }

    public ProyectoInmobiliario Agregar(ProyectoInmobiliario proyecto)
    {
        lock (_candado)
        {
            var nuevo = Copiar(proyecto);
            nuevo.Id = _siguienteId++;
            _proyectos.Add(nuevo);
            return Copiar(nuevo);
        }
    }

    public bool Actualizar(ProyectoInmobiliario proyecto)
    {
        lock (_candado)
        {
            var indice = _proyectos.FindIndex(p => p.Id == proyecto.Id);
            if (indice < 0) return false;
            _proyectos[indice] = Copiar(proyecto);
            return true;
        }
    }

    public bool Eliminar(int id)
    {
        lock (_candado)
        {
            var proyecto = _proyectos.FirstOrDefault(p => p.Id == id);
            return proyecto != null && _proyectos.Remove(proyecto);
        }
    }

    private static ProyectoInmobiliario Copiar(ProyectoInmobiliario p) => new()
    {
        Id = p.Id, Nombre = p.Nombre, Promotor = p.Promotor, Municipio = p.Municipio,
        Tipo = p.Tipo, AreaMetrosCuadrados = p.AreaMetrosCuadrados,
        ValorEstimado = p.ValorEstimado, TieneLicencia = p.TieneLicencia,
        TieneConceptoUsoSuelo = p.TieneConceptoUsoSuelo, EstadoConsolidacion = p.EstadoConsolidacion
    };
}
