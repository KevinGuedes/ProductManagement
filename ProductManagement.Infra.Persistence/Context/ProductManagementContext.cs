using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infra.Persistence.Context
{
    public class ProductManagementContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductManagementContext(DbContextOptions<ProductManagementContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductManagementContext).Assembly);
        }
    }
}
