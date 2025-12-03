using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Repositorio.IRepositorio
{
    public interface IProductoRepositorio
    {
        ICollection<ProductoCategoriaDto> GetProductos();
        bool AgregarProducto(AgregarProductoCategoriaDto producto);
        bool ActualizarProducto(ActualizarProductoCategoriaDto producto);
        bool ActualizarCantidad(int id, int cantidad);
        Producto GetProductoById(int id);
        bool EliminarProducto(int id);
        
    }
}
