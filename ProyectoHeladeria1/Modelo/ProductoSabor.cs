using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    /// <summary>Sabores que ofrece un producto (solo aplica si su categoria LlevaSabor).</summary>
    [Table("ProductoSabor")]
    public class ProductoSabor
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        [ForeignKey("Sabor")]
        public int IdSabor { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Sabor Sabor { get; set; }
    }
}
