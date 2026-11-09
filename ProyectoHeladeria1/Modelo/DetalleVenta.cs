using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Detalle_Venta")]
    public class DetalleVenta
    {
        [Key]
        [Column("id_detalle_venta")]
        public int IdDetalleVenta { get; set; }

        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("id_sabor")]
        public int? IdSabor { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio_unitario", TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("IdVenta")]
        public virtual Venta Venta { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [ForeignKey("IdSabor")]
        public virtual Sabor? Sabor { get; set; }
    }
}
