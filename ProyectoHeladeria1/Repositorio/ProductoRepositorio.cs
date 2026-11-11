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
        public bool ActualizarProducto(ActualizarProductoCategoriaDto producto)
        {
            var productoActualizar = _context.Productos.FirstOrDefault(x => x.Id == producto.Id);
            productoActualizar.FechaCreacion = DateTime.Now;
            productoActualizar.Nombre = producto.Nombre;
            productoActualizar.Descripcion = producto.Descripcion;
            productoActualizar.Precio = producto.Precio;
            productoActualizar.Cantidad = producto.Cantidad;
            productoActualizar.Disponible = producto.Disponible;
            var actualizar = _context.Productos.Update(productoActualizar);
            _context.SaveChanges();

            var addProductoCategoria = _context.ProductoCategorias.FirstOrDefault(x => x.IdProducto == producto.Id);

            //Si no existe agregar.
            if(addProductoCategoria == null)
            {
                addProductoCategoria = new ProductoCategoria
                {
                    IdProducto = productoActualizar.Id,
                    IdCategoria = producto.Categorias.Id
                };

                _context.ProductoCategorias.Add(addProductoCategoria);
                _context.SaveChanges();
            }
            else
            {
                // sino actualizar
                addProductoCategoria.IdCategoria = producto.Categorias.Id;
                _context.ProductoCategorias.Update(addProductoCategoria);
                _context.SaveChanges();
            }

            SincronizarSabores(productoActualizar.Id, producto.Categorias.Id, producto.SaboresIds);

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
                Disponible = producto.Disponible,
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

            SincronizarSabores(addProducto.Id, producto.Categorias.Id, producto.SaboresIds);
            return agregar == null?false:true;
        }

        /// <summary>
        /// Deja en el producto exactamente los sabores indicados. Solo aplica si la categoria
        /// LlevaSabor; si no, se limpian. Si ids es null no se modifica nada.
        /// </summary>
        private void SincronizarSabores(int idProducto, int idCategoria, List<int>? ids)
        {
            if (ids == null) return;

            var llevaSabor = _context.Categorias.Any(c => c.Id == idCategoria && c.LlevaSabor);
            var deseados = llevaSabor
                ? ids.Distinct().Where(id => _context.Sabores.Any(s => s.Id == id)).ToList()
                : new List<int>();

            var actuales = _context.ProductoSabores.Where(x => x.IdProducto == idProducto).ToList();
            _context.ProductoSabores.RemoveRange(actuales.Where(x => !deseados.Contains(x.IdSabor)));

            foreach (var idSabor in deseados.Where(id => actuales.All(x => x.IdSabor != id)))
            {
                _context.ProductoSabores.Add(new ProductoSabor { IdProducto = idProducto, IdSabor = idSabor });
            }
            _context.SaveChanges();
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
            Cantidad = p.Cantidad,
            Disponible = p.Disponible,
            LlevaSabor = p.ProductoCategorias.Any(pc => pc.Categoria.LlevaSabor),

            // Mapeo de la lista de categorías
            Categorias = p.ProductoCategorias
                .Select(pc => new Categoria // pc es ProductoCategoria
                {
                    Id = pc.Categoria.Id,
                    Nombre = pc.Categoria.Nombre,
                    LlevaSabor = pc.Categoria.LlevaSabor
                })
                .ToList(),

            Sabores = p.ProductoSabores
                .Select(ps => new Sabor { Id = ps.Sabor.Id, Nombre = ps.Sabor.Nombre })
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
            var productoCategoria = _context.ProductoCategorias.Where(x => x.IdProducto == id).ToList();

            foreach (var item in productoCategoria)
            {
                _context.ProductoCategorias.Remove(item);
                _context.SaveChanges();
            }

            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return producto == null ? false : true;
        }

        public bool ActualizarCantidad(int id, int cantidad)
        {
            var producto = _context.Productos.Where(x => x.Id == id).FirstOrDefault();
            producto.Cantidad = producto.Cantidad - cantidad;
            _context.Productos.Update(producto);
            _context.SaveChanges();

            return true;
        }
    }
}
