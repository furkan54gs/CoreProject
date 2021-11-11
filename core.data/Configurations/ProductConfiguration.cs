using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.entity;

namespace core.data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(m => m.ProductId);

            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);

            builder.Property(m => m.Rate).HasMaxLength(5);

            builder.Property(m => m.DateAdded).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");    // mssql getdate()
                                                                                              // sqlite date('now')
            builder.Property(m => m.Price).HasPrecision(10, 2);
        }
    }
}