using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Repositorio.IRepositorio
{
    public interface IProductoCategoriaRepositorio
    {
        ICollection<ProductoCategoria> GetProductosCategorias();
        
    }
}
