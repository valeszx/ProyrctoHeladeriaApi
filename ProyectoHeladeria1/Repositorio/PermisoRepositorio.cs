using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class PermisoRepositorio: IPermisoRepositorio
    {

        // readonly se utiliza para almacenar una instancia de la clase
        private readonly AplicationDbContext _context;

        public PermisoRepositorio(AplicationDbContext context)
        {
            _context = context;
        }

        public Permiso ObtenerPermisoPorUsuario(int id)
        {
            return _context.Permisos.FirstOrDefault(x => x.IdLogin == id);
        }
    }
}
