using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepositorio _CatRepo;
        public CategoriaController(ICategoriaRepositorio CatRepo)
        {
            _CatRepo = CatRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listadoCategoria =_CatRepo.GetCategorias();
            return Ok(listadoCategoria);
        }

        [HttpGet("{id}/ProductoId")]
        public IActionResult ObtenerCategoriaPorId(int id)
        {
            var listadoCategoria = _CatRepo.GetCategoriaById(id);
            return Ok(listadoCategoria);
        }

        [HttpPut]
        public IActionResult Actualizar(Categoria categoria)
        {
            var resultado = _CatRepo.ActualizarCategoria(categoria);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult Agregar([FromBody] Categoria categoria)
        {
            var resultado = _CatRepo.AgregarCategoria(categoria);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        { 
            var resultado = _CatRepo.EliminarCategoria(id);
            return Ok(resultado);
        }
    }
}
