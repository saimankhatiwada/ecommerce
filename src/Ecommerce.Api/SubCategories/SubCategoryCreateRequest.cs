namespace Ecommerce.Api.SubCategories;

public sealed record SubCategoryCreateRequest(string Name, Guid CategoryId);
