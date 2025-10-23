using Proyecto_Heladeria.Modelo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProyectoHeladeria1.Modelo
{
    [Table("Permiso")]
    public class Permiso
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Logins")]
        public int IdLogin { get; set; }
        public bool TienePermiso {  get; set; }

        public virtual Login Logins { get; set; }
    }
}
