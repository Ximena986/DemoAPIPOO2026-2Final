using DemoAPIPOO2026_2.DTOs;
using DemoAPIPOO2026_2.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DemoAPIPOO2026_2.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProyectosController : ControllerBase
{
    private readonly IProyectoServicio _servicio;
    public ProyectosController(IProyectoServicio servicio) => _servicio = servicio;
    [HttpGet]
    [Authorize(Roles = "Administrador,Consulta")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObtenerTodos([FromQuery] string? municipio, [FromQuery] string? tipo) => Ok(_servicio.ObtenerTodos(municipio, tipo));
    [HttpGet("consolidados")]
    [Authorize(Roles = "Administrador,Consulta")]
    public IActionResult ObtenerConsolidados() => Ok(_servicio.ObtenerConsolidados());
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrador,Consulta")]
    public IActionResult ObtenerPorId(int id)
    {
        var proyecto = _servicio.ObtenerPorId(id);
        if (proyecto == null) return NotFound(new { mensaje = "Proyecto no encontrado." });
        return Ok(proyecto);
    }
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Crear([FromBody] ProyectoDTO dto)
    {
        try
        {
            var nuevo = _servicio.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
        }
        catch (ArgumentException ex) { return BadRequest(new { mensaje = ex.Message }); }
    }
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public IActionResult Actualizar(int id, [FromBody] ProyectoDTO dto)
    {
        try
        {
            var actualizado = _servicio.Actualizar(id, dto);
            if (actualizado == null) return NotFound(new { mensaje = "Proyecto no encontrado." });
            return Ok(actualizado);
        }
        catch (ArgumentException ex) { return BadRequest(new { mensaje = ex.Message }); }
    }
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        if (!_servicio.Eliminar(id)) return NotFound(new { mensaje = "Proyecto no encontrado." });
        return NoContent();
    }
}
