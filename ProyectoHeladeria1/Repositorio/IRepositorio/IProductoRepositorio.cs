using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Repositorio.IRepositorio
{
    public interface IProductoRepositorio
    {
        ICollection<Producto> GetProductos();
        bool AgregarProducto(Producto producto);
        bool ActualizarProducto(Producto producto);
        Producto GetProductoById(int id);
        
    }
}
