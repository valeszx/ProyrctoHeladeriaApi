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
                throw new InvalidOperationException("La compra debe tener al menos un articulo.");
            if (!_context.Logins.Any(l => l.Id == dto.IdUsuario))
                throw new InvalidOperationException("El usuario no existe.");

            using var tx = _context.Database.BeginTransaction();

            var compra = new Compra { IdUsuario = dto.IdUsuario, Fecha = DateTime.Now, Detalles = new List<DetalleCompra>() };
            _context.Compras.Add(compra);

            // Unidades que entran al inventario por cada linea (insumos: presentaciones x unidades por presentacion).
            var unidadesPorLinea = new Dictionary<DetalleCompra, decimal>();

            foreach (var d in dto.Detalles)
            {
                InventarioHelper.ValidarCantidad(d.Cantidad);
                if ((d.IdProducto == null) == (d.IdInsumo == null))
                    throw new InvalidOperationException("Cada linea debe indicar un producto o un insumo (no ambos).");

                var linea = new DetalleCompra { Cantidad = d.Cantidad, CostoUnitario = d.CostoUnitario };

                if (d.IdInsumo != null)
                {
                    var insumo = _context.Insumos.FirstOrDefault(x => x.Id == d.IdInsumo)
                        ?? throw new InvalidOperationException($"El insumo {d.IdInsumo} no existe.");

                    var porPresentacion = d.UnidadesPorPresentacion ?? insumo.UnidadesPorPresentacion;
                    if (porPresentacion < 1)
                        throw new InvalidOperationException("Las unidades por presentacion deben ser al menos 1.");

                    var unidades = (decimal)d.Cantidad * porPresentacion;
                    InventarioHelper.AjustarStockInsumo(_context, insumo.Id, unidades);

                    linea.IdInsumo = insumo.Id;
                    linea.UnidadesPorPresentacion = porPresentacion;
                    unidadesPorLinea[linea] = unidades;
                }
                else
                {
                    InventarioHelper.AjustarStock(_context, d.IdProducto!.Value, d.Cantidad);
                    linea.IdProducto = d.IdProducto;
                    unidadesPorLinea[linea] = d.Cantidad;
                }
                compra.Detalles.Add(linea);
            }
            _context.SaveChanges();

            foreach (var (linea, unidades) in unidadesPorLinea)
            {
                _context.Movimientos.Add(new Movimiento
                {
                    IdProducto = linea.IdProducto,
                    IdInsumo = linea.IdInsumo,
                    IdCompra = compra.IdCompra,
                    Tipo = InventarioHelper.TipoCompra,
                    Cantidad = unidades,
                    Fecha = compra.Fecha
                });
            }

            // El stock repuesto resuelve las alertas pendientes de esos articulos.
            var idsProducto = compra.Detalles.Where(d => d.IdProducto != null).Select(d => d.IdProducto!.Value).ToList();
            var idsInsumo = compra.Detalles.Where(d => d.IdInsumo != null).Select(d => d.IdInsumo!.Value).ToList();
            var minimo = InventarioHelper.StockMinimo;

            var resueltas = _context.Notificaciones.Where(n => !n.Atendida &&
                ((n.IdProducto != null && idsProducto.Contains(n.IdProducto.Value) && n.Producto!.Cantidad > minimo) ||
                 (n.IdInsumo != null && idsInsumo.Contains(n.IdInsumo.Value) && n.Insumo!.Stock > n.Insumo.StockMinimo)));
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
                    Producto = d.Producto != null ? d.Producto.Nombre : null,
                    IdInsumo = d.IdInsumo,
                    Insumo = d.Insumo != null ? d.Insumo.Nombre : null,
                    Cantidad = d.Cantidad,
                    UnidadesPorPresentacion = d.UnidadesPorPresentacion,
                    CostoUnitario = d.CostoUnitario
                }).ToList()
            });
    }
}
