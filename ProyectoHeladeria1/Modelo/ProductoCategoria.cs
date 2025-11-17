using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("ProductoCategoria")]
    public class ProductoCategoria
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
