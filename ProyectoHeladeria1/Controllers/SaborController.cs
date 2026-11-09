using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaborController : ControllerBase
    {
        private readonly ISaborRepositorio _saborRepo;
        public SaborController(ISaborRepositorio saborRepo)
        {
            _saborRepo = saborRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_saborRepo.GetSabores());
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var sabor = _saborRepo.GetSaborById(id);
            return sabor == null ? NotFound() : Ok(sabor);
        }

        [HttpPost]
        public IActionResult Agregar([FromBody] Sabor sabor)
        {
            return Ok(_saborRepo.AgregarSabor(sabor));
        }

        [HttpPut]
        public IActionResult Actualizar([FromBody] Sabor sabor)
        {
            return Ok(_saborRepo.ActualizarSabor(sabor));
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return Ok(_saborRepo.EliminarSabor(id));
        }
    }
}
