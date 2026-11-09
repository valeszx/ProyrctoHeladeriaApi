using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionRepositorio _repo;
        public NotificacionController(INotificacionRepositorio repo) { _repo = repo; }

        /// <summary>?atendida=false devuelve solo las pendientes.</summary>
        [HttpGet]
        public IActionResult Get([FromQuery] bool? atendida) => Ok(_repo.GetNotificaciones(atendida));

        [HttpPut("{id}/atendida")]
        public IActionResult MarcarAtendida(int id) =>
            _repo.MarcarAtendida(id) ? Ok(true) : NotFound();

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id) =>
            _repo.Eliminar(id) ? Ok(true) : NotFound();
    }
}
