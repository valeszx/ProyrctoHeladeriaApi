using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    /// <summary>Receta de un producto: que insumos consume cada unidad vendida.</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        private readonly IInsumoRepositorio _repo;
        public RecetaController(IInsumoRepositorio repo) { _repo = repo; }

        [HttpGet("{idProducto}")]
        public IActionResult Get(int idProducto) => Ok(_repo.GetReceta(idProducto));

        /// <summary>Reemplaza toda la receta. Body: [{ idInsumo, cantidad }]. Lista vacia = sin insumos.</summary>
        [HttpPut("{idProducto}")]
        public IActionResult Guardar(int idProducto, [FromBody] List<RecetaItemDto> receta)
        {
            try { _repo.GuardarReceta(idProducto, receta); return Ok(true); }
            catch (InvalidOperationException ex) { return BadRequest(new { mensaje = ex.Message }); }
        }
    }
}
