using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class NotificacionRepositorio : INotificacionRepositorio
    {
        private readonly AplicationDbContext _context;
        public NotificacionRepositorio(AplicationDbContext context) { _context = context; }

        public ICollection<NotificacionDto> GetNotificaciones(bool? atendida)
        {
            var q = _context.Notificaciones.AsNoTracking().AsQueryable();
            if (atendida != null) q = q.Where(n => n.Atendida == atendida);

            return q.OrderByDescending(n => n.Fecha).Select(n => new NotificacionDto
            {
                IdNotificacion = n.IdNotificacion,
                IdProducto = n.IdProducto,
                Producto = n.Producto.Nombre,
                Mensaje = n.Mensaje,
                Fecha = n.Fecha,
                Atendida = n.Atendida
            }).ToList();
        }

        public bool MarcarAtendida(int id)
        {
            var n = _context.Notificaciones.FirstOrDefault(x => x.IdNotificacion == id);
            if (n == null) return false;
            n.Atendida = true;
            _context.SaveChanges();
            return true;
        }

        public bool Eliminar(int id)
        {
            var n = _context.Notificaciones.FirstOrDefault(x => x.IdNotificacion == id);
            if (n == null) return false;
            _context.Notificaciones.Remove(n);
            _context.SaveChanges();
            return true;
        }
    }
}
