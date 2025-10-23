using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Heladeria.Repositorio.IRepositorio;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoRepositorio _repos;
        public PermisoController(IPermisoRepositorio repos)
        {
            _repos = repos;
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPermisoPorUsuario(int id) {
            var permiso = _repos.ObtenerPermisoPorUsuario(id);
            return Ok(permiso);
        }
    }
}
