using Ecommerce.Domain.Categories;
using Ecommerce.Domain.SubCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

internal sealed class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable("sub_categories");

        builder.HasKey(subCategory => subCategory.Id);

        builder.Property(subCategory => subCategory.Id)
            .HasConversion(id => id.Value, value => new SubCategoryId(value));

        builder.Property(subCategory => subCategory.Name)
            .HasMaxLength(100)
            .HasConversion(name => name.Value, value => new Domain.SubCategories.Name(value));

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(category => category.CategoryId);
    }

}
