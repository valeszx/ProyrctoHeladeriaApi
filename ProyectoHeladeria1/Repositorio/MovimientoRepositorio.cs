using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class MovimientoRepositorio : IMovimientoRepositorio
    {
        private readonly AplicationDbContext _context;
        public MovimientoRepositorio(AplicationDbContext context) { _context = context; }

        public ICollection<MovimientoDto> GetMovimientos(int? idProducto, int? idInsumo, string? tipo, DateTime? desde, DateTime? hasta)
        {
            var q = _context.Movimientos.AsNoTracking().AsQueryable();
            if (idProducto != null) q = q.Where(m => m.IdProducto == idProducto);
            if (idInsumo != null) q = q.Where(m => m.IdInsumo == idInsumo);
            if (!string.IsNullOrWhiteSpace(tipo)) q = q.Where(m => m.Tipo == tipo);
            if (desde != null) q = q.Where(m => m.Fecha >= desde);
            if (hasta != null) q = q.Where(m => m.Fecha <= hasta);

            return q.OrderByDescending(m => m.Fecha).Select(m => new MovimientoDto
            {
                IdMovimiento = m.IdMovimiento,
                IdProducto = m.IdProducto,
                Producto = m.Producto != null ? m.Producto.Nombre : null,
                IdInsumo = m.IdInsumo,
                Insumo = m.Insumo != null ? m.Insumo.Nombre : null,
                IdVenta = m.IdVenta,
                IdCompra = m.IdCompra,
                IdPerdida = m.IdPerdida,
                Tipo = m.Tipo,
                Cantidad = m.Cantidad,
                Fecha = m.Fecha
            }).ToList();
        }
    }
}
