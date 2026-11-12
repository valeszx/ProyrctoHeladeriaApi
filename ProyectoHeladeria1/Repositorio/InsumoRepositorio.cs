using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class InsumoRepositorio : IInsumoRepositorio
    {
        private readonly AplicationDbContext _context;
        public InsumoRepositorio(AplicationDbContext context) { _context = context; }

        public ICollection<Insumo> GetInsumos(bool incluirInactivos)
        {
            var q = _context.Insumos.AsNoTracking().AsQueryable();
            if (!incluirInactivos) q = q.Where(x => x.Activo);
            return q.OrderBy(x => x.Nombre).ToList();
        }

        public Insumo? GetInsumoById(int id) =>
            _context.Insumos.AsNoTracking().FirstOrDefault(x => x.Id == id);

        public int AgregarInsumo(Insumo insumo)
        {
            Validar(insumo);
            var nuevo = new Insumo
            {
                Nombre = insumo.Nombre.Trim(),
                Unidad = string.IsNullOrWhiteSpace(insumo.Unidad) ? "unidad" : insumo.Unidad.Trim(),
                Presentacion = insumo.Presentacion?.Trim(),
                UnidadesPorPresentacion = insumo.UnidadesPorPresentacion,
                Stock = insumo.Stock, // stock inicial; despues solo cambia con compras y ventas
                StockMinimo = insumo.StockMinimo,
                Activo = true,
                FechaCreacion = DateTime.Now
            };
            _context.Insumos.Add(nuevo);
            _context.SaveChanges();
            return nuevo.Id;
        }

        /// <summary>Edita los datos del insumo. El Stock NO se modifica aqui (solo compras y ventas lo mueven).</summary>
        public bool ActualizarInsumo(Insumo insumo)
        {
            var actual = _context.Insumos.FirstOrDefault(x => x.Id == insumo.Id);
            if (actual == null) return false;
            Validar(insumo);

            actual.Nombre = insumo.Nombre.Trim();
            actual.Unidad = string.IsNullOrWhiteSpace(insumo.Unidad) ? "unidad" : insumo.Unidad.Trim();
            actual.Presentacion = insumo.Presentacion?.Trim();
            actual.UnidadesPorPresentacion = insumo.UnidadesPorPresentacion;
            actual.StockMinimo = insumo.StockMinimo;
            actual.Activo = insumo.Activo;
            _context.SaveChanges();
            return true;
        }

        /// <summary>Si el insumo ya tiene historial (compras, movimientos o recetas) se desactiva; si no, se borra.</summary>
        public bool EliminarInsumo(int id)
        {
            var insumo = _context.Insumos.FirstOrDefault(x => x.Id == id);
            if (insumo == null) return false;

            var tieneHistorial = _context.DetalleCompras.Any(x => x.IdInsumo == id)
                || _context.Movimientos.Any(x => x.IdInsumo == id)
                || _context.Notificaciones.Any(x => x.IdInsumo == id)
                || _context.ProductoInsumos.Any(x => x.IdInsumo == id);

            if (tieneHistorial) insumo.Activo = false;
            else _context.Insumos.Remove(insumo);

            _context.SaveChanges();
            return true;
        }

        public ICollection<RecetaItemDto> GetReceta(int idProducto) =>
            _context.ProductoInsumos.AsNoTracking()
                .Where(x => x.IdProducto == idProducto)
                .OrderBy(x => x.Insumo.Nombre)
                .Select(x => new RecetaItemDto
                {
                    IdInsumo = x.IdInsumo,
                    Insumo = x.Insumo.Nombre,
                    Unidad = x.Insumo.Unidad,
                    Cantidad = x.Cantidad
                }).ToList();

        /// <summary>Reemplaza la receta completa del producto. Lista vacia = el producto no consume insumos.</summary>
        public void GuardarReceta(int idProducto, List<RecetaItemDto> receta)
        {
            if (!_context.Productos.Any(p => p.Id == idProducto))
                throw new InvalidOperationException("El producto no existe.");

            receta ??= new List<RecetaItemDto>();
            if (receta.Any(r => r.Cantidad <= 0))
                throw new InvalidOperationException("La cantidad de cada insumo debe ser mayor a cero.");
            if (receta.GroupBy(r => r.IdInsumo).Any(g => g.Count() > 1))
                throw new InvalidOperationException("Un insumo no puede repetirse en la receta.");

            var ids = receta.Select(r => r.IdInsumo).ToList();
            if (_context.Insumos.Count(i => ids.Contains(i.Id)) != ids.Count)
                throw new InvalidOperationException("Alguno de los insumos no existe.");

            var actuales = _context.ProductoInsumos.Where(x => x.IdProducto == idProducto).ToList();
            _context.ProductoInsumos.RemoveRange(actuales.Where(a => !ids.Contains(a.IdInsumo)));

            foreach (var item in receta)
            {
                var existente = actuales.FirstOrDefault(a => a.IdInsumo == item.IdInsumo);
                if (existente != null) existente.Cantidad = item.Cantidad;
                else _context.ProductoInsumos.Add(new ProductoInsumo
                {
                    IdProducto = idProducto,
                    IdInsumo = item.IdInsumo,
                    Cantidad = item.Cantidad
                });
            }
            _context.SaveChanges();
        }

        private static void Validar(Insumo i)
        {
            if (string.IsNullOrWhiteSpace(i.Nombre))
                throw new InvalidOperationException("El nombre es obligatorio.");
            if (i.UnidadesPorPresentacion < 1)
                throw new InvalidOperationException("Las unidades por presentacion deben ser al menos 1.");
            if (i.Stock < 0 || i.StockMinimo < 0)
                throw new InvalidOperationException("El stock y el stock minimo no pueden ser negativos.");
        }
    }
}
