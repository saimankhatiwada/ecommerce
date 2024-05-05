using Ecommerce.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Id)
            .HasConversion(id => id.Value, value => new CategoryId(value));

        builder.Property(category => category.Name)
            .HasMaxLength(100)
            .HasConversion(name => name.Value, value => new Name(value));
    }

}
