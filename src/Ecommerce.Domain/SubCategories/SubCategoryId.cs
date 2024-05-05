namespace Ecommerce.Domain.SubCategories;

public record SubCategoryId(Guid Value)
{
    public static SubCategoryId New() => new(Guid.NewGuid());
}
