using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using ProyectoHeladeria1.Modelo;
using ProyectoHeladeria1.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly AplicationDbContext _context;
        public CategoriaRepositorio(AplicationDbContext context)
        {
            _context = context;  
        }

        /// <summary>
        /// Actualizar los productos
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool ActualizarCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            var actualizar = _context.Categorias.Update(categoria);
            _context.SaveChanges();
            return actualizar == null?false:true;
        }

        /// <summary>
        /// Se agrega el producto a la base de datos.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool AgregarCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            var agregar = _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return agregar == null?false:true;
        }

        /// <summary>
        /// Obtengo un producto de la bd por un Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Categoria GetCategoriaById(int id)
        {
            return _context.Categorias.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Obtengo todos los productos de la BD.
        /// </summary>
        /// <returns></returns>
        public ICollection<Categoria> GetCategorias()
        {
            return _context.Categorias.ToList();
        }

        /// <summary>
        /// Se agrega el producto a la base de datos.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool EliminarCategoria(int id)
        {
           
            var categoria = _context.Categorias.FirstOrDefault(x => x.Id == id);
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return categoria == null ? false : true;
        }

    }
}
