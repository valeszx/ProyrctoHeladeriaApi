using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Repositorio.IRepositorio
{
    public interface ISaborRepositorio
    {
        ICollection<Sabor> GetSabores();
        Sabor? GetSaborById(int id);
        bool AgregarSabor(Sabor sabor);
        bool ActualizarSabor(Sabor sabor);
        bool EliminarSabor(int id);
    }
}
