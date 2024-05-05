using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Categories;

public sealed class Category : Entity<CategoryId>
{
    private Category(CategoryId id, Name name) : base(id)
    {
        Name = name;
    }

    private Category()
    {
    }

    public Name Name { get; private set; }

    public static Category Create(Name name)
    {
        var category = new Category(CategoryId.New(), name);

        return category;
    }

    public void UpdateName(Name name)
    {
        Name = name;
    } 
}
