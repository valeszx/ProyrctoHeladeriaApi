using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Modelo;

namespace Proyecto_Heladeria.Data
{
    public class AplicationDbContext : DbContext
    {
        //puente entre la app y la base de datos :clase central que se utiliza para interactuar con la base de datos
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) 
        {
            
        }
        //sirve agregar las tablas DbSet
       public DbSet<Login> Logins { get; set; }


    }
}
