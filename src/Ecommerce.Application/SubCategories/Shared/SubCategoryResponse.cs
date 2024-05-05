namespace Ecommerce.Application.SubCategories.Shared;

public sealed record SubCategoryResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public Guid CategoryId { get; init; }
}
