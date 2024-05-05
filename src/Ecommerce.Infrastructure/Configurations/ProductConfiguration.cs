using Ecommerce.Domain.Categories;
using Ecommerce.Domain.Products;
using Ecommerce.Domain.Shared;
using Ecommerce.Domain.SubCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id)
            .HasConversion(id => id.Value, value => new ProductId(value));

        builder.Property(product => product.Name)
            .HasMaxLength(100)
            .HasConversion(name => name.Value, value => new Domain.Products.Name(value));

        builder.Property(product => product.Description)
            .HasMaxLength(15000)
            .HasConversion(description => description.Value, value => new Description(value));

        builder.Property(product => product.Status)
            .HasConversion(status => status.Value, value => Status.FromStatus(value));

        builder.OwnsOne(product => product.Money, moneyBuilder => moneyBuilder.Property(money => money.Currency)
            .HasConversion(currency => currency.Code, code => Currency.FromCode(code)));

        builder.Property(product => product.ImageName)
            .HasConversion(imageName => imageName.Value, value => new ImageName(value));

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(category => category.CategoryId);

        builder.HasOne<SubCategory>()
            .WithMany()
            .HasForeignKey(subCategory => subCategory.SubCategoryId);
    }

}
