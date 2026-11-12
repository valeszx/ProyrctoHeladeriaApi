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

            var movimientos = new List<Movimiento>();

            foreach (var d in dto.Detalles)
            {
                InventarioHelper.ValidarCantidad(d.Cantidad);
                ValidarSabor(d);

                var producto = _context.Productos.FirstOrDefault(x => x.Id == d.IdProducto)
                    ?? throw new InvalidOperationException($"El producto {d.IdProducto} no existe.");

                // Siempre se descuenta la Cantidad del producto...
                InventarioHelper.AjustarStock(_context, d.IdProducto, -d.Cantidad);
                movimientos.Add(new Movimiento
                {
                    IdProducto = d.IdProducto,
                    Tipo = InventarioHelper.TipoVenta,
                    Cantidad = d.Cantidad,
                    Fecha = venta.Fecha
                });

                // ...y ademas los insumos de su receta (vaso, pajilla, cono...), si tiene.
                var receta = _context.ProductoInsumos.Where(r => r.IdProducto == d.IdProducto).ToList();
                foreach (var r in receta)
                {
                    var consumo = r.Cantidad * d.Cantidad;
                    InventarioHelper.AjustarStockInsumo(_context, r.IdInsumo, -consumo);
                    movimientos.Add(new Movimiento
                    {
                        IdInsumo = r.IdInsumo,
                        Tipo = InventarioHelper.TipoVenta,
                        Cantidad = consumo,
                        Fecha = venta.Fecha
                    });
                }

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

            foreach (var m in movimientos)
            {
                m.IdVenta = venta.IdVenta;
                _context.Movimientos.Add(m);
            }
            _context.SaveChanges();
            tx.Commit();
            return venta.IdVenta;
        }

        /// <summary>El sabor es obligatorio si el producto tiene sabores y debe ser uno de ellos.</summary>
        private void ValidarSabor(AgregarDetalleVentaDto d)
        {
            var saboresProducto = _context.ProductoSabores
                .Where(x => x.IdProducto == d.IdProducto).Select(x => x.IdSabor).ToList();

            if (d.IdSabor == null)
            {
                if (saboresProducto.Count > 0)
                    throw new InvalidOperationException("Debe seleccionar un sabor para este producto.");
                return;
            }

            if (!saboresProducto.Contains(d.IdSabor.Value))
                throw new InvalidOperationException("El sabor seleccionado no esta disponible para este producto.");
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
