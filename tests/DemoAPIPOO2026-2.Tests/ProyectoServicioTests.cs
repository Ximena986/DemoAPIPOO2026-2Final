using DemoAPIPOO2026_2.Contexto;
using DemoAPIPOO2026_2.DTOs;
using DemoAPIPOO2026_2.Estrategias;
using DemoAPIPOO2026_2.Modelos;
using DemoAPIPOO2026_2.Repositorios;
using DemoAPIPOO2026_2.Servicios;
using Xunit;
namespace DemoAPIPOO2026_2.Tests;
public class ProyectoServicioTests
{
    private static ProyectoServicio CrearServicio() => new(new ProyectoRepositorio(new DatosInmobiliariosContexto()), new ConsolidacionFactory());
    private static ProyectoDTO DtoValido() => new()
    {
        Nombre = "Proyecto de prueba", Promotor = "Promotora de prueba", Municipio = "La Ceja",
        Tipo = "Rural", AreaMetrosCuadrados = 1500, ValorEstimado = 900000000,
        TieneLicencia = false, TieneConceptoUsoSuelo = true
    };
    [Fact]
    public void ContextoIniciaConOchoProyectos() => Assert.Equal(8, new DatosInmobiliariosContexto().ObtenerTodos().Count);
    [Fact]
    public void ObtenerTodosSinFiltrosDevuelveOcho() => Assert.Equal(8, CrearServicio().ObtenerTodos(null, null).Count);
    [Fact]
    public void FiltroPorMunicipioEsIgnorandoMayusculas() => Assert.Single(CrearServicio().ObtenerTodos("rionegro", null));
    [Fact]
    public void FiltroPorTipoRuralDevuelveCuatro() => Assert.Equal(4, CrearServicio().ObtenerTodos(null, "Rural").Count);
    [Fact]
    public void ObtenerConsolidadosExcluyePendientes() => Assert.Equal(6, CrearServicio().ObtenerConsolidados().Count);
    [Fact]
    public void ObtenerPorIdInexistenteDevuelveNull() => Assert.Null(CrearServicio().ObtenerPorId(999));
    [Fact]
    public void CrearAsignaIdYCalculaEstado() 
    {
        var creado = CrearServicio().Crear(DtoValido());
        Assert.Equal(9, creado.Id);
        Assert.Equal("Consolidado", creado.EstadoConsolidacion);
    }
    [Fact]
    public void CrearRechazaMunicipioNoPermitido() 
    {
        var dto = DtoValido(); dto.Municipio = "Medellín";
        Assert.Throws<ArgumentException>(() => CrearServicio().Crear(dto));
    }
    [Fact]
    public void CrearRechazaTipoNoSoportado()
    {
        var dto = DtoValido(); dto.Tipo = "Mixto";
        Assert.Throws<ArgumentException>(() => CrearServicio().Crear(dto));
    }
    [Fact]
    public void CrearRechazaAreaCero()
    {
        var dto = DtoValido(); dto.AreaMetrosCuadrados = 0;
        Assert.Throws<ArgumentException>(() => CrearServicio().Crear(dto));
    }
    [Fact]
    public void CrearRechazaNombreVacio()
    {
        var dto = DtoValido(); dto.Nombre = " ";
        Assert.Throws<ArgumentException>(() => CrearServicio().Crear(dto));
    }
    [Fact]
    public void ActualizarModificaProyectoExistente()
    {
        var servicio = CrearServicio(); var dto = DtoValido(); dto.Nombre = "Nombre actualizado";
        var actualizado = servicio.Actualizar(1, dto);
        Assert.NotNull(actualizado); Assert.Equal("Nombre actualizado", actualizado!.Nombre);
    }
    [Fact]
    public void ActualizarIdInexistenteDevuelveNull() => Assert.Null(CrearServicio().Actualizar(999, DtoValido()));
    [Fact]
    public void EliminarProyectoExistenteDevuelveTrue()
    {
        var servicio = CrearServicio(); Assert.True(servicio.Eliminar(1)); Assert.Null(servicio.ObtenerPorId(1));
    }
    [Fact]
    public void EliminarIdInexistenteDevuelveFalse() => Assert.False(CrearServicio().Eliminar(999));
}
