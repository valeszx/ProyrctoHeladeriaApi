using Proyecto_Heladeria.Modelo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Venta")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("total", TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Login Usuario { get; set; }

        public virtual ICollection<DetalleVenta>? Detalles { get; set; }
    }
}
