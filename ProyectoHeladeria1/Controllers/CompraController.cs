using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraRepositorio _repo;
        public CompraController(ICompraRepositorio repo) { _repo = repo; }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.GetCompras());

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var compra = _repo.GetCompraById(id);
            return compra == null ? NotFound() : Ok(compra);
        }

        /// <summary>Registra la compra, suma stock y genera movimientos.</summary>
        [HttpPost]
        public IActionResult Agregar([FromBody] AgregarCompraDto compra)
        {
            try { return Ok(_repo.AgregarCompra(compra)); }
            catch (InvalidOperationException ex) { return BadRequest(new { mensaje = ex.Message }); }
        }
    }
}
