using DemoAPIPOO2026_2.Modelos;
namespace DemoAPIPOO2026_2.Repositorios;
public interface IProyectoRepositorio
{
    List<ProyectoInmobiliario> ObtenerTodos();
    ProyectoInmobiliario? ObtenerPorId(int id);
    ProyectoInmobiliario Crear(ProyectoInmobiliario proyecto);
    bool Actualizar(ProyectoInmobiliario proyecto);
    bool Eliminar(int id);
}
