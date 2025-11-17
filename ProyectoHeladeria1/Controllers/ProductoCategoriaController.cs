using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Heladeria.Repositorio.IRepositorio;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoCategoriaController : ControllerBase
    {
        private readonly IProductoCategoriaRepositorio _repos; 
        public ProductoCategoriaController(IProductoCategoriaRepositorio repos)
        {
            _repos = repos;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listadoLogin = _repos.GetProductosCategorias();
            return Ok(listadoLogin);
        }


    }
}
