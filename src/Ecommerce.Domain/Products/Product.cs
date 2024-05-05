using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Categories;
using Ecommerce.Domain.Shared;
using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Domain.Products;

public sealed class Product : Entity<ProductId>
{
    private Product(
        ProductId id, 
        CategoryId categoryId, 
        SubCategoryId subCategoryId,
        Name name,
        Description description,
        Status status,
        Money money,
        ImageName imageName) 
        : base(id)
    {
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        Name = name;
        Description = description;
        Status = status;
        Money = money;
        ImageName = imageName;
    }

    private Product()
    {
    }

    public CategoryId CategoryId { get; private set; }

    public SubCategoryId SubCategoryId { get; private set; }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public Status Status { get; private set; }

    public Money Money { get; private set; }

    public ImageName ImageName { get; private set; }

    public static Product Create(
        CategoryId categoryId, 
        SubCategoryId subCategoryId,
        Name name,
        Description description,
        Status status,
        Money money,
        ImageName imageName)
    {
        var product = new Product(
            ProductId.New(), 
            categoryId,
            subCategoryId,
            name,
            description,
            status,
            money,
            imageName);

        return product;
    }

    public void Update(
        CategoryId categoryId, 
        SubCategoryId subCategoryId,
        Name name,
        Description description,
        Status status,
        Money money,
        ImageName imageName)
    {
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        Name = name;
        Description = description;
        Status = status;
        Money = money;
        ImageName = imageName;
    }
}
