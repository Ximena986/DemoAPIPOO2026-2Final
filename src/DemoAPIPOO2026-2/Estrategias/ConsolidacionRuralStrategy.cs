using DemoAPIPOO2026_2.Modelos;
namespace DemoAPIPOO2026_2.Estrategias;
public class ConsolidacionRuralStrategy : IConsolidacionStrategy
{
    public string Evaluar(ProyectoInmobiliario proyecto) =>
        proyecto.TieneConceptoUsoSuelo
            ? "Consolidado" : "Pendiente de concepto de uso del suelo";
}
