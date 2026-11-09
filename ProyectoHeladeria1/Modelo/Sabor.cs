using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoHeladeria1.Modelo
{
    [Table("Sabores")]
    public class Sabor
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
