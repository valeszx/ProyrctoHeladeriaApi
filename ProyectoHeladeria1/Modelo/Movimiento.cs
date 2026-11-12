using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Movimiento")]
    public class Movimiento
    {
        [Key]
        [Column("id_movimiento")]
        public int IdMovimiento { get; set; }

        /// <summary>Producto afectado (null si el movimiento es de un insumo).</summary>
        [Column("id_producto")]
        public int? IdProducto { get; set; }

        /// <summary>Insumo afectado (null si el movimiento es de un producto).</summary>
        [Column("id_insumo")]
        public int? IdInsumo { get; set; }

        [Column("id_venta")]
        public int? IdVenta { get; set; }

        [Column("id_compra")]
        public int? IdCompra { get; set; }

        [Column("id_perdida")]
        public int? IdPerdida { get; set; }

        /// <summary>Venta, Compra o Perdida.</summary>
        [Column("tipo")]
        public string Tipo { get; set; }

        /// <summary>Unidades que entran o salen (en insumos, unidades sueltas; no presentaciones).</summary>
        [Column("cantidad", TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("IdInsumo")]
        public virtual Insumo? Insumo { get; set; }

        [ForeignKey("IdVenta")]
        public virtual Venta? Venta { get; set; }

        [ForeignKey("IdCompra")]
        public virtual Compra? Compra { get; set; }

        [ForeignKey("IdPerdida")]
        public virtual Perdida? Perdida { get; set; }
    }
}
