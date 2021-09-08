using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.entity;

namespace core.data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder.Property(m=>m.Title).IsRequired().HasMaxLength(80);

            builder.Property(m=>m.Description).IsRequired().HasMaxLength(800);

            builder.Property(m=>m.DateAdded).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");    // mssql getdate()
                                                                                            // sqlite date('now')
        }
    }
}