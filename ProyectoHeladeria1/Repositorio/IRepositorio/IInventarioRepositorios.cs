using ProyectoHeladeria1.Dto;

namespace ProyectoHeladeria1.Repositorio.IRepositorio
{
    public interface IVentaRepositorio
    {
        ICollection<VentaDto> GetVentas();
        VentaDto? GetVentaById(int id);
        int AgregarVenta(AgregarVentaDto venta);
    }

    public interface ICompraRepositorio
    {
        ICollection<CompraDto> GetCompras();
        CompraDto? GetCompraById(int id);
        int AgregarCompra(AgregarCompraDto compra);
    }

    public interface IPerdidaRepositorio
    {
        ICollection<PerdidaDto> GetPerdidas();
        PerdidaDto? GetPerdidaById(int id);
        int AgregarPerdida(AgregarPerdidaDto perdida);
    }

    public interface IMovimientoRepositorio
    {
        ICollection<MovimientoDto> GetMovimientos(int? idProducto, string? tipo, DateTime? desde, DateTime? hasta);
    }

    public interface INotificacionRepositorio
    {
        ICollection<NotificacionDto> GetNotificaciones(bool? atendida);
        bool MarcarAtendida(int id);
        bool Eliminar(int id);
    }
}
