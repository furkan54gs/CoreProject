using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.entity;

namespace core.data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(m=>m.ProductId);

            builder.Property(m=>m.Name).IsRequired().HasMaxLength(100);

            builder.Property(m=>m.DateAdded).HasDefaultValueSql("date('now')");  // mssql getdate()
            // builder.Property(m=>m.DateAdded).HasDefaultValueSql ("date('now')");   // sqlite date('now')
        }
    }
}