using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerdidaController : ControllerBase
    {
        private readonly IPerdidaRepositorio _repo;
        public PerdidaController(IPerdidaRepositorio repo) { _repo = repo; }

        [HttpGet]
        public IActionResult Get() => Ok(_repo.GetPerdidas());

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var perdida = _repo.GetPerdidaById(id);
            return perdida == null ? NotFound() : Ok(perdida);
        }

        /// <summary>Registra la perdida, descuenta stock y genera movimiento/notificacion.</summary>
        [HttpPost]
        public IActionResult Agregar([FromBody] AgregarPerdidaDto perdida)
        {
            try { return Ok(_repo.AgregarPerdida(perdida)); }
            catch (InvalidOperationException ex) { return BadRequest(new { mensaje = ex.Message }); }
        }
    }
}
