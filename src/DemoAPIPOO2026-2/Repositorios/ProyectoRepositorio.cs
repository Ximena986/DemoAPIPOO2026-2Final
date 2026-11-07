using DemoAPIPOO2026_2.Contexto;
using DemoAPIPOO2026_2.Modelos;
namespace DemoAPIPOO2026_2.Repositorios;
public class ProyectoRepositorio : IProyectoRepositorio
{
    private readonly DatosInmobiliariosContexto _contexto;
    public ProyectoRepositorio(DatosInmobiliariosContexto contexto) => _contexto = contexto;
    public List<ProyectoInmobiliario> ObtenerTodos() => _contexto.ObtenerTodos().OrderBy(p => p.Id).ToList();
    public ProyectoInmobiliario? ObtenerPorId(int id) => _contexto.BuscarPorId(id);
    public ProyectoInmobiliario Crear(ProyectoInmobiliario proyecto) => _contexto.Agregar(proyecto);
    public bool Actualizar(ProyectoInmobiliario proyecto) => _contexto.Actualizar(proyecto);
    public bool Eliminar(int id) => _contexto.Eliminar(id);
}
