using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Categories;

namespace Ecommerce.Domain.SubCategories;

public sealed class SubCategory : Entity<SubCategoryId>
{
    private SubCategory(SubCategoryId id, Name name, CategoryId categoryId) : base(id)
    {
        Name = name;
        CategoryId = categoryId;
    }

    private SubCategory()
    {
    }

    public Name Name { get; private set; }

    public CategoryId CategoryId { get; private set; }

    public static SubCategory Create(Name name, CategoryId categoryId)
    {
        var subCategory = new SubCategory(SubCategoryId.New(), name, categoryId);

        return subCategory;
    }

    public void Update(Name name, CategoryId categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }
}
