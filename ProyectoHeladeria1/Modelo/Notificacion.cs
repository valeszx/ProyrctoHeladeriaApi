using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Notificacion")]
    public class Notificacion
    {
        [Key]
        [Column("id_notificacion")]
        public int IdNotificacion { get; set; }

        /// <summary>Producto con stock bajo (null si la alerta es de un insumo).</summary>
        [Column("id_producto")]
        public int? IdProducto { get; set; }

        /// <summary>Insumo con stock bajo (null si la alerta es de un producto).</summary>
        [Column("id_insumo")]
        public int? IdInsumo { get; set; }

        [Column("mensaje")]
        public string Mensaje { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("atendida")]
        public bool Atendida { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("IdInsumo")]
        public virtual Insumo? Insumo { get; set; }
    }
}
