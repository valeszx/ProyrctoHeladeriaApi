using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaRepositorio _repo;
        public VentaController(IVentaRepositorio repo) { _repo = repo; }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.GetVentas());

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var venta = _repo.GetVentaById(id);
            return venta == null ? NotFound() : Ok(venta);
        }

        /// <summary>Registra la venta, descuenta stock y genera movimientos/notificaciones.</summary>
        [HttpPost]
        public IActionResult Agregar([FromBody] AgregarVentaDto venta)
        {
            try { return Ok(_repo.AgregarVenta(venta)); }
            catch (InvalidOperationException ex) { return BadRequest(new { mensaje = ex.Message }); }
        }
    }
}
