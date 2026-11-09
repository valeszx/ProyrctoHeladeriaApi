using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class SaborRepositorio : ISaborRepositorio
    {
        private readonly AplicationDbContext _context;
        public SaborRepositorio(AplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Sabor> GetSabores()
        {
            return _context.Sabores.OrderBy(x => x.Nombre).ToList();
        }

        public Sabor? GetSaborById(int id)
        {
            return _context.Sabores.FirstOrDefault(x => x.Id == id);
        }

        public bool AgregarSabor(Sabor sabor)
        {
            _context.Sabores.Add(new Sabor { Nombre = sabor.Nombre });
            return _context.SaveChanges() > 0;
        }

        public bool ActualizarSabor(Sabor sabor)
        {
            var actual = _context.Sabores.FirstOrDefault(x => x.Id == sabor.Id);
            if (actual == null) return false;

            actual.Nombre = sabor.Nombre;
            return _context.SaveChanges() >= 0;
        }

        public bool EliminarSabor(int id)
        {
            var sabor = _context.Sabores.FirstOrDefault(x => x.Id == id);
            if (sabor == null) return false;

            _context.Sabores.Remove(sabor);
            return _context.SaveChanges() > 0;
        }
    }
}
