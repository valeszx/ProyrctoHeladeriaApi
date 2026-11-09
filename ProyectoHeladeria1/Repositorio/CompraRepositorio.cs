using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class CompraRepositorio : ICompraRepositorio
    {
        private readonly AplicationDbContext _context;
        public CompraRepositorio(AplicationDbContext context) { _context = context; }

        public ICollection<CompraDto> GetCompras() =>
            Proyectar(_context.Compras.AsNoTracking()).OrderByDescending(c => c.Fecha).ToList();

        public CompraDto? GetCompraById(int id) =>
            Proyectar(_context.Compras.AsNoTracking().Where(c => c.IdCompra == id)).FirstOrDefault();

        public int AgregarCompra(AgregarCompraDto dto)
        {
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                throw new InvalidOperationException("La compra debe tener al menos un producto.");
            if (!_context.Logins.Any(l => l.Id == dto.IdUsuario))
                throw new InvalidOperationException("El usuario no existe.");

            using var tx = _context.Database.BeginTransaction();

            var compra = new Compra { IdUsuario = dto.IdUsuario, Fecha = DateTime.Now, Detalles = new List<DetalleCompra>() };
            _context.Compras.Add(compra);

            foreach (var d in dto.Detalles)
            {
                InventarioHelper.ValidarCantidad(d.Cantidad);
                InventarioHelper.AjustarStock(_context, d.IdProducto, d.Cantidad);
                compra.Detalles.Add(new DetalleCompra
                {
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    CostoUnitario = d.CostoUnitario
                });
            }
            _context.SaveChanges();

            foreach (var d in compra.Detalles)
            {
                _context.Movimientos.Add(new Movimiento
                {
                    IdProducto = d.IdProducto,
                    IdCompra = compra.IdCompra,
                    Tipo = InventarioHelper.TipoCompra,
                    Cantidad = d.Cantidad,
                    Fecha = compra.Fecha
                });
            }

            // El stock repuesto resuelve alertas pendientes de esos productos.
            var ids = compra.Detalles.Select(d => d.IdProducto).ToList();
            var minimo = InventarioHelper.StockMinimo;
            var resueltas = _context.Notificaciones
                .Where(n => !n.Atendida && ids.Contains(n.IdProducto) && n.Producto.Cantidad > minimo);
            foreach (var n in resueltas) n.Atendida = true;

            _context.SaveChanges();
            tx.Commit();
            return compra.IdCompra;
        }

        private static IQueryable<CompraDto> Proyectar(IQueryable<Compra> q) =>
            q.Select(c => new CompraDto
            {
                IdCompra = c.IdCompra,
                IdUsuario = c.IdUsuario,
                Usuario = c.Usuario.Usuario,
                Fecha = c.Fecha,
                Total = c.Detalles!.Sum(d => d.Cantidad * d.CostoUnitario),
                Detalles = c.Detalles!.Select(d => new DetalleCompraDto
                {
                    IdDetalleCompra = d.IdDetalleCompra,
                    IdProducto = d.IdProducto,
                    Producto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    CostoUnitario = d.CostoUnitario
                }).ToList()
            });
    }
}
