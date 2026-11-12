using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    /// <summary>Material que se compra para elaborar los productos (vasos, conos, pajillas...).</summary>
    [Table("Insumo")]
    public class Insumo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Unidad { get; set; } = "unidad";

        /// <summary>Como se compra: Bolsa, Caja, Paquete...</summary>
        public string? Presentacion { get; set; }

        /// <summary>Unidades que trae cada presentacion (ej. 50 conos por bolsa).</summary>
        public int UnidadesPorPresentacion { get; set; } = 1;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Stock { get; set; }

        /// <summary>Se genera una alerta cuando Stock llega a este valor o menos.</summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal StockMinimo { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime? FechaCreacion { get; set; }

        [NotMapped]
        public bool BajoStock => Stock <= StockMinimo;
    }
}
