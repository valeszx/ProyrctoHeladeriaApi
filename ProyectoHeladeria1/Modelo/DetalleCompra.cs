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

        /// <summary>Producto comprado (null si la linea es de un insumo).</summary>
        [Column("id_producto")]
        public int? IdProducto { get; set; }

        /// <summary>Insumo comprado (null si la linea es de un producto).</summary>
        [Column("id_insumo")]
        public int? IdInsumo { get; set; }

        /// <summary>Presentaciones compradas (ej. 2 bolsas).</summary>
        [Column("cantidad")]
        public int Cantidad { get; set; }

        /// <summary>Unidades por presentacion en esta compra (ej. 50). Solo insumos.</summary>
        [Column("unidades_por_presentacion")]
        public int? UnidadesPorPresentacion { get; set; }

        /// <summary>Costo de cada presentacion.</summary>
        [Column("costo_unitario", TypeName = "decimal(10,2)")]
        public decimal CostoUnitario { get; set; }

        [ForeignKey("IdCompra")]
        public virtual Compra Compra { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("IdInsumo")]
        public virtual Insumo? Insumo { get; set; }
    }
}
