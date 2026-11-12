using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsumoController : ControllerBase
    {
        private readonly IInsumoRepositorio _repo;
        public InsumoController(IInsumoRepositorio repo) { _repo = repo; }

        /// <summary>Por defecto solo activos (para la pantalla de compras). ?incluirInactivos=true para todos.</summary>
        [HttpGet]
        public IActionResult Get([FromQuery] bool incluirInactivos = false) =>
            Ok(_repo.GetInsumos(incluirInactivos));

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var insumo = _repo.GetInsumoById(id);
            return insumo == null ? NotFound() : Ok(insumo);
        }

        /// <summary>Devuelve el id del insumo creado.</summary>
        [HttpPost]
        public IActionResult Agregar([FromBody] Insumo insumo)
        {
            try { return Ok(_repo.AgregarInsumo(insumo)); }
            catch (InvalidOperationException ex) { return BadRequest(new { mensaje = ex.Message }); }
        }

        /// <summary>No modifica el stock; el stock solo cambia con compras y ventas.</summary>
        [HttpPut]
        public IActionResult Actualizar([FromBody] Insumo insumo)
        {
            try { return _repo.ActualizarInsumo(insumo) ? Ok(true) : NotFound(); }
            catch (InvalidOperationException ex) { return BadRequest(new { mensaje = ex.Message }); }
        }

        /// <summary>Si el insumo tiene historial se desactiva en vez de borrarse.</summary>
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id) =>
            _repo.EliminarInsumo(id) ? Ok(true) : NotFound();
    }
}
