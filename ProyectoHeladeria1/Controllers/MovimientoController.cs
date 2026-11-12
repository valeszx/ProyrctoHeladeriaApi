using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoRepositorio _repo;
        public MovimientoController(IMovimientoRepositorio repo) { _repo = repo; }

        /// <summary>Filtros opcionales: idProducto, tipo (Venta/Compra/Perdida), desde, hasta.</summary>
        [HttpGet]
        public IActionResult Get([FromQuery] int? idProducto, [FromQuery] int? idInsumo, [FromQuery] string? tipo,
                                 [FromQuery] DateTime? desde, [FromQuery] DateTime? hasta)
        {
            return Ok(_repo.GetMovimientos(idProducto, idInsumo, tipo, desde, hasta));
        }
    }
}
