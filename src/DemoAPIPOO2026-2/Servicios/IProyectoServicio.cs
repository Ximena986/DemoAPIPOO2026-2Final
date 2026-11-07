using DemoAPIPOO2026_2.DTOs;
using DemoAPIPOO2026_2.Modelos;
namespace DemoAPIPOO2026_2.Servicios;
public interface IProyectoServicio
{
    List<ProyectoInmobiliario> ObtenerTodos(string? municipio, string? tipo);
    List<ProyectoInmobiliario> ObtenerConsolidados();
    ProyectoInmobiliario? ObtenerPorId(int id);
    ProyectoInmobiliario Crear(ProyectoDTO dto);
    ProyectoInmobiliario? Actualizar(int id, ProyectoDTO dto);
    bool Eliminar(int id);
}
