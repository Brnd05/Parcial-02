using Microsoft.EntityFrameworkCore;


namespace Productos.Models
{
    // La clase ProductosContext hereda de DbContext y representa el contexto de la base de datos
    public class ProductosContext : DbContext
    {
        // Constructor que recibe opciones de configuración para la base de datos
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {
        }

        // DbSet representa la tabla "Productos" en la base de datos
        public DbSet<Producto> Productos { get; set; } = null!;

        // Método para configurar el modelo de datos cuando se crea la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llama al método base para aplicar configuraciones estándar
            base.OnModelCreating(modelBuilder);

            // Define un índice único en la propiedad "Nombre" de la entidad "Producto"
            modelBuilder.Entity<Producto>().HasIndex(c => c.Nombre).IsUnique();
        }
    }
}