using DemoAPIPOO2026_2.Modelos;
namespace DemoAPIPOO2026_2.Estrategias;
public class ConsolidacionUrbanaStrategy : IConsolidacionStrategy
{
    public string Evaluar(ProyectoInmobiliario proyecto) =>
        proyecto.TieneLicencia && proyecto.TieneConceptoUsoSuelo
            ? "Consolidado" : "Pendiente de requisitos urbanos";
}
