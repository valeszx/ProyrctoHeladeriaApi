using Proyecto_Heladeria.Modelo;
using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Repositorio.IRepositorio
{
    public interface IPermisoRepositorio
    {
        Permiso  ObtenerPermisoPorUsuario(int id);
    }
}
