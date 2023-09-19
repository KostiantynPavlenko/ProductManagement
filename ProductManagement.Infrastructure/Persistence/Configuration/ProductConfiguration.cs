using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain.Categories;
using ProductManagement.Domain.Products;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Infrastructure.Persistence.Configuration;

public class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.CategoryId).IsRequired();

        builder
            .HasOne(x => x.Category)
            .WithOne(a => a.Product)
            .HasForeignKey<Product>(x => x.CategoryId);

        builder.OwnsOne(p => p.Sku, skuBuilder =>
        {
            skuBuilder.Property(s => s.Value)
                .IsRequired();
        });
        
        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Amount)
                .IsRequired();
        });
    }
}