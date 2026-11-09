using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Modelo;
using ProyectoHeladeria1.Modelo;

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
       public DbSet<Producto> Productos { get; set; }
       public DbSet<Permiso> Permisos { get; set; }
       public DbSet<Categoria> Categorias { get; set; }
       public DbSet<ProductoCategoria> ProductoCategorias { get; set; }
       public DbSet<Sabor> Sabores { get; set; }
       public DbSet<Compra> Compras { get; set; }
       public DbSet<DetalleCompra> DetalleCompras { get; set; }
       public DbSet<Venta> Ventas { get; set; }
       public DbSet<DetalleVenta> DetalleVentas { get; set; }
       public DbSet<Perdida> Perdidas { get; set; }
       public DbSet<Movimiento> Movimientos { get; set; }
       public DbSet<Notificacion> Notificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // SQL Server no permite multiples rutas de borrado en cascada
            // (p.ej. Login -> Compra -> Movimiento y Login -> Perdida -> Movimiento),
            // por eso las nuevas relaciones usan Restrict.
            var nuevas = new[]
            {
                typeof(Compra), typeof(DetalleCompra), typeof(Venta), typeof(DetalleVenta),
                typeof(Perdida), typeof(Movimiento), typeof(Notificacion)
            };

            // En la BD "cantidad" es decimal(10,2); en el codigo se maneja como int.
            foreach (var tipo in new[] { typeof(DetalleCompra), typeof(DetalleVenta), typeof(Perdida), typeof(Movimiento) })
            {
                modelBuilder.Entity(tipo).Property("Cantidad").HasConversion<decimal>().HasColumnType("decimal(10,2)");
            }

            foreach (var tipo in nuevas)
            {
                foreach (var fk in modelBuilder.Entity(tipo).Metadata.GetForeignKeys())
                {
                    fk.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

    }
}
