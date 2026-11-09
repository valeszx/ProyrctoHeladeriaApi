using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Dto;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class PerdidaRepositorio : IPerdidaRepositorio
    {
        private readonly AplicationDbContext _context;
        public PerdidaRepositorio(AplicationDbContext context) { _context = context; }

        public ICollection<PerdidaDto> GetPerdidas() =>
            Proyectar(_context.Perdidas.AsNoTracking()).OrderByDescending(p => p.Fecha).ToList();

        public PerdidaDto? GetPerdidaById(int id) =>
            Proyectar(_context.Perdidas.AsNoTracking().Where(p => p.IdPerdida == id)).FirstOrDefault();

        public int AgregarPerdida(AgregarPerdidaDto dto)
        {
            InventarioHelper.ValidarCantidad(dto.Cantidad);
            if (string.IsNullOrWhiteSpace(dto.Motivo))
                throw new InvalidOperationException("El motivo es obligatorio.");
            if (!_context.Logins.Any(l => l.Id == dto.IdUsuario))
                throw new InvalidOperationException("El usuario no existe.");

            using var tx = _context.Database.BeginTransaction();

            InventarioHelper.AjustarStock(_context, dto.IdProducto, -dto.Cantidad);
            var perdida = new Perdida
            {
                IdUsuario = dto.IdUsuario,
                IdProducto = dto.IdProducto,
                Cantidad = dto.Cantidad,
                Motivo = dto.Motivo.Trim(),
                Fecha = DateTime.Now
            };
            _context.Perdidas.Add(perdida);
            _context.SaveChanges();

            _context.Movimientos.Add(new Movimiento
            {
                IdProducto = perdida.IdProducto,
                IdPerdida = perdida.IdPerdida,
                Tipo = InventarioHelper.TipoPerdida,
                Cantidad = perdida.Cantidad,
                Fecha = perdida.Fecha
            });
            _context.SaveChanges();
            tx.Commit();
            return perdida.IdPerdida;
        }

        private static IQueryable<PerdidaDto> Proyectar(IQueryable<Perdida> q) =>
            q.Select(p => new PerdidaDto
            {
                IdPerdida = p.IdPerdida,
                IdUsuario = p.IdUsuario,
                Usuario = p.Usuario.Usuario,
                IdProducto = p.IdProducto,
                Producto = p.Producto.Nombre,
                Cantidad = p.Cantidad,
                Motivo = p.Motivo,
                Fecha = p.Fecha
            });
    }
}
