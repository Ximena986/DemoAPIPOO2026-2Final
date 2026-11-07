using DemoAPIPOO2026_2.Estrategias;
using DemoAPIPOO2026_2.Modelos;
using Xunit;
namespace DemoAPIPOO2026_2.Tests;
public class ConsolidacionTests
{
    [Theory]
    [InlineData(true, true, "Consolidado")]
    [InlineData(true, false, "Pendiente de requisitos urbanos")]
    [InlineData(false, true, "Pendiente de requisitos urbanos")]
    [InlineData(false, false, "Pendiente de requisitos urbanos")]
    public void EstrategiaUrbanaEvaluaAmbosRequisitos(bool licencia, bool uso, string esperado)
    {
        var proyecto = new ProyectoInmobiliario { TieneLicencia = licencia, TieneConceptoUsoSuelo = uso };
        Assert.Equal(esperado, new ConsolidacionUrbanaStrategy().Evaluar(proyecto));
    }
    [Theory]
    [InlineData(true, "Consolidado")]
    [InlineData(false, "Pendiente de concepto de uso del suelo")]
    public void EstrategiaRuralEvaluaConcepto(bool uso, string esperado)
    {
        var proyecto = new ProyectoInmobiliario { TieneConceptoUsoSuelo = uso };
        Assert.Equal(esperado, new ConsolidacionRuralStrategy().Evaluar(proyecto));
    }
    [Theory]
    [InlineData("Urbano", typeof(ConsolidacionUrbanaStrategy))]
    [InlineData("Rural", typeof(ConsolidacionRuralStrategy))]
    [InlineData(" rural ", typeof(ConsolidacionRuralStrategy))]
    public void FactoryDevuelveEstrategiaCorrecta(string tipo, Type esperado) =>
        Assert.IsType(esperado, new ConsolidacionFactory().Crear(tipo));
    [Theory]
    [InlineData("Mixto")]
    [InlineData("")]
    [InlineData("   ")]
    public void FactoryRechazaTipoNoValido(string tipo) =>
        Assert.Throws<ArgumentException>(() => new ConsolidacionFactory().Crear(tipo));
}
