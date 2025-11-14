using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio
    {
        ICollection<Categoria> GetCategorias();
        bool AgregarCategoria(Categoria producto);
        bool ActualizarCategoria(Categoria producto);
        Categoria GetCategoriaById(int id);
        bool EliminarCategoria(int id);
        
    }
}
