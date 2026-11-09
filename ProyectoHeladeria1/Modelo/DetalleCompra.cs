using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Detalle_Compra")]
    public class DetalleCompra
    {
        [Key]
        [Column("id_detalle_compra")]
        public int IdDetalleCompra { get; set; }

        [Column("id_compra")]
        public int IdCompra { get; set; }

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("costo_unitario", TypeName = "decimal(10,2)")]
        public decimal CostoUnitario { get; set; }

        [ForeignKey("IdCompra")]
        public virtual Compra Compra { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
    }
}
