using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class VentaRepositorio : IVentaRepositorio
    {
        private readonly AplicationDbContext _context;
        public VentaRepositorio(AplicationDbContext context) { _context = context; }

        public ICollection<VentaDto> GetVentas() =>
            Proyectar(_context.Ventas.AsNoTracking()).OrderByDescending(v => v.Fecha).ToList();

        public VentaDto? GetVentaById(int id) =>
            Proyectar(_context.Ventas.AsNoTracking().Where(v => v.IdVenta == id)).FirstOrDefault();

        public int AgregarVenta(AgregarVentaDto dto)
        {
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                throw new InvalidOperationException("La venta debe tener al menos un producto.");
            if (!_context.Logins.Any(l => l.Id == dto.IdUsuario))
                throw new InvalidOperationException("El usuario no existe.");

            using var tx = _context.Database.BeginTransaction();

            var venta = new Venta { IdUsuario = dto.IdUsuario, Fecha = DateTime.Now, Detalles = new List<DetalleVenta>() };
            _context.Ventas.Add(venta);

            foreach (var d in dto.Detalles)
            {
                InventarioHelper.ValidarCantidad(d.Cantidad);
                if (d.IdSabor != null && !_context.Sabores.Any(s => s.Id == d.IdSabor))
                    throw new InvalidOperationException($"El sabor {d.IdSabor} no existe.");

                var producto = InventarioHelper.AjustarStock(_context, d.IdProducto, -d.Cantidad);
                venta.Detalles.Add(new DetalleVenta
                {
                    IdProducto = d.IdProducto,
                    IdSabor = d.IdSabor,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario ?? producto.Precio
                });
            }
            venta.Total = venta.Detalles.Sum(x => x.Cantidad * x.PrecioUnitario);
            _context.SaveChanges(); // genera IdVenta

            foreach (var d in venta.Detalles)
            {
                _context.Movimientos.Add(new Movimiento
                {
                    IdProducto = d.IdProducto,
                    IdVenta = venta.IdVenta,
                    Tipo = InventarioHelper.TipoVenta,
                    Cantidad = d.Cantidad,
                    Fecha = venta.Fecha
                });
            }
            _context.SaveChanges();
            tx.Commit();
            return venta.IdVenta;
        }

        private static IQueryable<VentaDto> Proyectar(IQueryable<Venta> q) =>
            q.Select(v => new VentaDto
            {
                IdVenta = v.IdVenta,
                IdUsuario = v.IdUsuario,
                Usuario = v.Usuario.Usuario,
                Fecha = v.Fecha,
                Total = v.Total,
                Detalles = v.Detalles!.Select(d => new DetalleVentaDto
                {
                    IdDetalleVenta = d.IdDetalleVenta,
                    IdProducto = d.IdProducto,
                    Producto = d.Producto.Nombre,
                    IdSabor = d.IdSabor,
                    Sabor = d.Sabor != null ? d.Sabor.Nombre : null,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()
            });
    }
}
