using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }

        public decimal Precio {  get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<ProductoCategoria>? ProductoCategorias { get; set; }
    }
}
