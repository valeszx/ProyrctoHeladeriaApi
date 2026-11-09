using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Repositorio
{
    /// <summary>Reglas compartidas de inventario (stock, disponibilidad y alertas).</summary>
    internal static class InventarioHelper
    {
        public const int StockMinimo = 5;
        public const string TipoVenta = "Venta";
        public const string TipoCompra = "Compra";
        public const string TipoPerdida = "Perdida";

        /// <summary>
        /// Suma (delta positivo) o resta (delta negativo) stock y
        /// crea una notificacion si queda por debajo del minimo. No hace SaveChanges.
        /// </summary>
        public static Producto AjustarStock(AplicationDbContext context, int idProducto, int delta)
        {
            var producto = context.Productos.FirstOrDefault(x => x.Id == idProducto)
                ?? throw new InvalidOperationException($"El producto {idProducto} no existe.");

            var nuevo = producto.Cantidad + delta;
            if (nuevo < 0)
                throw new InvalidOperationException(
                    $"Stock insuficiente de '{producto.Nombre}': disponible {producto.Cantidad}, solicitado {-delta}.");

            producto.Cantidad = nuevo;

            if (delta < 0 && nuevo <= StockMinimo)
            {
                var pendiente = context.Notificaciones.Any(n => n.IdProducto == idProducto && !n.Atendida)
                    || context.Notificaciones.Local.Any(n => n.IdProducto == idProducto && !n.Atendida);
                if (!pendiente)
                {
                    context.Notificaciones.Add(new Notificacion
                    {
                        IdProducto = idProducto,
                        Mensaje = nuevo == 0
                            ? $"El producto '{producto.Nombre}' se agoto."
                            : $"Quedan {nuevo} unidades de '{producto.Nombre}'.",
                        Fecha = DateTime.Now,
                        Atendida = false
                    });
                }
            }
            return producto;
        }

        public static void ValidarCantidad(int cantidad)
        {
            if (cantidad <= 0) throw new InvalidOperationException("La cantidad debe ser mayor a cero.");
        }
    }
}
