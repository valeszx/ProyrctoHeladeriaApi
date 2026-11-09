using Proyecto_Heladeria.Modelo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Compra")]
    public class Compra
    {
        [Key]
        [Column("id_compra")]
        public int IdCompra { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Login Usuario { get; set; }

        public virtual ICollection<DetalleCompra>? Detalles { get; set; }
    }
}
