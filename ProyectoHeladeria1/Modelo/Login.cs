using System.ComponentModel.DataAnnotations;

namespace Proyecto_Heladeria.Modelo
{
    public class Login
    {
        [Key]
        public int Id {  get; set; }
        public String Usuario {  get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }

        

        
    }
}
