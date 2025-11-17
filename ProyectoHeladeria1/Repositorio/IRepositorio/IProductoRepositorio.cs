using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Repositorio.IRepositorio
{
    public interface IProductoRepositorio
    {
        ICollection<ProductoCategoriaDto> GetProductos();
        bool AgregarProducto(AgregarProductoCategoriaDto producto);
        bool ActualizarProducto(Producto producto);
        Producto GetProductoById(int id);
        bool EliminarProducto(int id);
        
    }
}
