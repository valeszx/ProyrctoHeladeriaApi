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

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("mensaje")]
        public string Mensaje { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("atendida")]
        public bool Atendida { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
    }
}
