using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string TipoCategoria { get; set; }
        public string? Ruta {  get; set; }
        /// <summary>Si es true, los productos de esta categoria pueden llevar sabor (ej. Conos).</summary>
        public bool LlevaSabor { get; set; }

        public virtual ICollection<ProductoCategoria>? ProductoCategorias { get; set; }
    }
}
