using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Infrastructure.Persistance.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(e => e.CategoryName, "CategoryName");

        builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
        builder.Property(e => e.CategoryName).HasMaxLength(15);
        builder.Property(e => e.Description).HasColumnType("ntext");
        builder.Property(e => e.Picture).HasColumnType("image");
    }
}
