using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Modelo;

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
        ICollection<MovimientoDto> GetMovimientos(int? idProducto, int? idInsumo, string? tipo, DateTime? desde, DateTime? hasta);
    }

    public interface IInsumoRepositorio
    {
        ICollection<Insumo> GetInsumos(bool incluirInactivos);
        Insumo? GetInsumoById(int id);
        int AgregarInsumo(Insumo insumo);
        bool ActualizarInsumo(Insumo insumo);
        bool EliminarInsumo(int id);

        ICollection<RecetaItemDto> GetReceta(int idProducto);
        void GuardarReceta(int idProducto, List<RecetaItemDto> receta);
    }

    public interface INotificacionRepositorio
    {
        ICollection<NotificacionDto> GetNotificaciones(bool? atendida);
        bool MarcarAtendida(int id);
        bool Eliminar(int id);
    }
}
