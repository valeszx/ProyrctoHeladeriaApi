using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    /// <summary>Receta: insumo que consume una unidad vendida del producto.</summary>
    [Table("ProductoInsumo")]
    public class ProductoInsumo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        [ForeignKey("Insumo")]
        public int IdInsumo { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; } = 1;

        public virtual Producto Producto { get; set; }
        public virtual Insumo Insumo { get; set; }
    }
}
