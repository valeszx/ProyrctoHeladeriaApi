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

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("id_venta")]
        public int? IdVenta { get; set; }

        [Column("id_compra")]
        public int? IdCompra { get; set; }

        [Column("id_perdida")]
        public int? IdPerdida { get; set; }

        /// <summary>Venta, Compra o Perdida.</summary>
        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [ForeignKey("IdVenta")]
        public virtual Venta? Venta { get; set; }

        [ForeignKey("IdCompra")]
        public virtual Compra? Compra { get; set; }

        [ForeignKey("IdPerdida")]
        public virtual Perdida? Perdida { get; set; }
    }
}
