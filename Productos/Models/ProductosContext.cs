using Microsoft.EntityFrameworkCore;

namespace Productos.Models
{
    public class ProductosContext : DbContext
    {

        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>().HasIndex(c => c.Nombre).IsUnique();

        }
    

    }
}
