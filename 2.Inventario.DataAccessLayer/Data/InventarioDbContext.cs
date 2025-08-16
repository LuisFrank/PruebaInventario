using Microsoft.EntityFrameworkCore;
using _2.Inventario.DataAccessLayer.Models;
using _2.Inventario.DataAccessLayer.Data.Configurations;

namespace _2.Inventario.DataAccessLayer.Data
{
    public class InventarioDbContext : DbContext
    {
        public InventarioDbContext(DbContextOptions<InventarioDbContext> options) : base(options)
        {
        }

        public DbSet<MovInventario> MovInventarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplicar configuraciones
            modelBuilder.ApplyConfiguration(new MovInventarioConfiguration());
        }
    }
}