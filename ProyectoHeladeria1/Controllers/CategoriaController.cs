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
        public IActionResult Actualizar([FromForm] Categoria categoria, IFormFile? imagen)
        {
            var categoriaActual = _CatRepo.GetCategoriaById(categoria.Id);
            // 2. Manejo de la Imagen (Solo si se envió un nuevo archivo)
            if (imagen != null && imagen.Length > 0)
            {
                // a. Opcional: Eliminar la imagen vieja (para limpiar espacio en el servidor)
                if (!string.IsNullOrEmpty(categoriaActual.Ruta))
                {
                    EliminarImagenAnterior(categoriaActual.Ruta);
                }

                // b. Guardar la nueva imagen y obtener su ruta pública
                string nuevaRuta = GuardarImagenEnServidor(imagen);
                categoria.Ruta = nuevaRuta; // Asignar la nueva ruta
            }

            var resultado = _CatRepo.ActualizarCategoria(categoria);
            return Ok(resultado);
        }

        [HttpPost]
        public  IActionResult Agregar([FromForm] Categoria categoria, IFormFile imagen)
        {
            if (imagen == null || imagen.Length == 0)
            {
                return BadRequest("Se requiere una imagen.");
            }

            // 1. Guardar el Archivo en el Servidor
            // Esto es un ejemplo. Debes definir una ruta de guardado.
            string rutaRelativa = GuardarImagenEnServidor(imagen);
            categoria.Ruta = rutaRelativa;

            var resultado = _CatRepo.AgregarCategoria(categoria);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        { 
            var resultado = _CatRepo.EliminarCategoria(id);
            return Ok(resultado);
        }

        // Ejemplo de función auxiliar para guardar la imagen (dentro del controlador o un servicio)
        private string GuardarImagenEnServidor(IFormFile file)
        {
            // Reemplaza con tu lógica real de Path.
            var nombreUnico = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categorias");

            // Asegúrate de que la carpeta exista
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            var rutaCompleta = Path.Combine(rutaCarpeta, nombreUnico);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            // Retorna la ruta relativa que se usará para mostrar la imagen en el frontend
            return $"/images/categorias/{nombreUnico}";
        }

        private void EliminarImagenAnterior(string rutaRelativa)
        {
            // 1. Obtener la ruta física completa
            var webRootPath = Path.Combine(Environment.CurrentDirectory, "wwwroot");
            var rutaFisica = Path.Combine(webRootPath, rutaRelativa.TrimStart('/')); // TrimStart elimina el '/' inicial

            // 2. Verificar si el archivo existe y eliminarlo
            if (System.IO.File.Exists(rutaFisica))
            {
                System.IO.File.Delete(rutaFisica);
            }
        }
    }
}
