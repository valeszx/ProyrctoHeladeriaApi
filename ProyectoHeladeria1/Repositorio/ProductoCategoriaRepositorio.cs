using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class ProductoCategoriaRepositorio : IProductoCategoriaRepositorio
    {
        private readonly AplicationDbContext _context;
        public ProductoCategoriaRepositorio(AplicationDbContext context)
        {
            _context = context;  
        }


        /// <summary>
        /// Obtengo todos los productos y categorias de la BD.
        /// </summary>
        /// <returns></returns>
        public ICollection<ProductoCategoria> GetProductosCategorias()
        {
            return _context.ProductoCategorias.ToList();
        }

    }
}
