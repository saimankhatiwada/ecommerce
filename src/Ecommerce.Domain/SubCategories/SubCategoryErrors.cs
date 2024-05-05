using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.SubCategories;

public static class SubCategoryErrors
{
    public static readonly Error NotFound = new(
        "SubCategory.NotFound",
        "The sub-category with the specified identifier was not found");
}
