using Proyecto_Heladeria.Modelo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Perdida")]
    public class Perdida
    {
        [Key]
        [Column("id_perdida")]
        public int IdPerdida { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("motivo")]
        public string Motivo { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Login Usuario { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
    }
}
