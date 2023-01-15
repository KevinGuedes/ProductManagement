using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infra.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.HasIndex(product => product.Code).IsUnique();

            builder.Property(product => product.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(product => product.Code).IsRequired();
            builder.Property(product => product.Description).IsRequired();
        }
    }
}
