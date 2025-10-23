using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepositorio _productRepo;
        public ProductoController(IProductoRepositorio productRepo)
        {
            _productRepo = productRepo;  
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listadoProducto = _productRepo.GetProductos();
            return Ok(listadoProducto);
        }

        [HttpGet("{id}/ProductoId")]
        public IActionResult ObtenerProductoPorId(int id)
        {
            var listadoProducto = _productRepo.GetProductoById(id);
            return Ok(listadoProducto);
        }

        [HttpPut]
        public IActionResult Actualizar(Producto producto)
        {
            var resultado = _productRepo.ActualizarProducto(producto);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult Agregar([FromBody] Producto producto)
        {
            var resultado = _productRepo.AgregarProducto(producto);
            return Ok(resultado);
        }
    }
}
