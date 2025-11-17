using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Dto;
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
            producto.FechaCreacion = DateTime.Now;
            var actualizar = _context.Productos.Update(producto);
            _context.SaveChanges();
            return actualizar == null?false:true;
        }

        /// <summary>
        /// Se agrega el producto a la base de datos.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool AgregarProducto(AgregarProductoCategoriaDto producto)
        {
            var addProducto = new Producto
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Cantidad = producto.Cantidad,
                FechaCreacion = DateTime.Now,
                Precio = producto.Precio,
            };

            var agregar = _context.Productos.Add(addProducto);
            _context.SaveChanges();

            var addProductoCategoria = new ProductoCategoria
            {
                IdProducto = addProducto.Id,
                IdCategoria = producto.Categorias.Id
            };

            _context.ProductoCategorias.Add(addProductoCategoria);
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
        public ICollection<ProductoCategoriaDto> GetProductos()
        {
            return _context.Productos
        .Select(p => new ProductoCategoriaDto
        {
            // Mapeo de campos directos del Producto
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Precio = p.Precio,

            // Mapeo de la lista de categorías
            Categorias = p.ProductoCategorias
                .Select(pc => new Categoria // pc es ProductoCategoria
                {
                    Id = pc.Categoria.Id,
                    Nombre = pc.Categoria.Nombre
                })
                .ToList()
        })
        .ToList();
        }

        /// <summary>
        /// Se agrega el producto a la base de datos.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool EliminarProducto(int id)
        {
           
            var producto = _context.Productos.FirstOrDefault(x => x.Id == id);
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return producto == null ? false : true;
        }

    }
}
