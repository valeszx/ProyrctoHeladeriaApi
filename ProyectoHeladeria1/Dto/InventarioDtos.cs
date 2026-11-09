namespace ProyectoHeladeria1.Dto
{
    // ---------- Ventas ----------
    public class AgregarVentaDto
    {
        public int IdUsuario { get; set; }
        public List<AgregarDetalleVentaDto> Detalles { get; set; } = new();
    }

    public class AgregarDetalleVentaDto
    {
        public int IdProducto { get; set; }
        public int? IdSabor { get; set; }
        public int Cantidad { get; set; }
        /// <summary>Si no se envia se usa el precio actual del producto.</summary>
        public decimal? PrecioUnitario { get; set; }
    }

    public class VentaDto
    {
        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaDto> Detalles { get; set; } = new();
    }

    public class DetalleVentaDto
    {
        public int IdDetalleVenta { get; set; }
        public int IdProducto { get; set; }
        public string? Producto { get; set; }
        public int? IdSabor { get; set; }
        public string? Sabor { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    // ---------- Compras ----------
    public class AgregarCompraDto
    {
        public int IdUsuario { get; set; }
        public List<AgregarDetalleCompraDto> Detalles { get; set; } = new();
    }

    public class AgregarDetalleCompraDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
    }

    public class CompraDto
    {
        public int IdCompra { get; set; }
        public int IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompraDto> Detalles { get; set; } = new();
    }

    public class DetalleCompraDto
    {
        public int IdDetalleCompra { get; set; }
        public int IdProducto { get; set; }
        public string? Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
    }

    // ---------- Perdidas ----------
    public class AgregarPerdidaDto
    {
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Motivo { get; set; } = "";
    }

    public class PerdidaDto
    {
        public int IdPerdida { get; set; }
        public int IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public int IdProducto { get; set; }
        public string? Producto { get; set; }
        public int Cantidad { get; set; }
        public string Motivo { get; set; } = "";
        public DateTime Fecha { get; set; }
    }

    // ---------- Movimientos ----------
    public class MovimientoDto
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string? Producto { get; set; }
        public int? IdVenta { get; set; }
        public int? IdCompra { get; set; }
        public int? IdPerdida { get; set; }
        public string Tipo { get; set; } = "";
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }

    // ---------- Notificaciones ----------
    public class NotificacionDto
    {
        public int IdNotificacion { get; set; }
        public int IdProducto { get; set; }
        public string? Producto { get; set; }
        public string Mensaje { get; set; } = "";
        public DateTime Fecha { get; set; }
        public bool Atendida { get; set; }
    }
}
