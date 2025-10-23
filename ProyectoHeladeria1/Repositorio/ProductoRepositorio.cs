using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly AplicationDbContext _context;
        public ProductoRepositorio(AplicationDbContext context)
        {
            _context = context;  
        }

        /// <summary>
        /// Actualizar los productos
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool ActualizarProducto(Producto producto)
        {
            var actualizar = _context.Productos.Update(producto);
            _context.SaveChanges();
            return actualizar != null;
        }

        /// <summary>
        /// Se agrega el producto a la base de datos.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool AgregarProducto(Producto producto)
        {
            producto.FechaCreacion = DateTime.Now;
            var agregar = _context.Productos.Add(producto);
            _context.SaveChanges();
            return agregar == null?false:true;
        }

        /// <summary>
        /// Obtengo un producto de la bd por un Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Producto GetProductoById(int id)
        {
            return _context.Productos.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Obtengo todos los productos de la BD.
        /// </summary>
        /// <returns></returns>
        public ICollection<Producto> GetProductos()
        {
            return _context.Productos.ToList();
        }
    }
}
