namespace Ecommerce.Application.Products.Shared;

public sealed record ProductResponse
{
    public Guid Id { get; init; }

    public Guid CategoryId { get; init; }

    public Guid SubCategoryId { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public string Status { get; init; }

    public decimal Amount { get; init; }

    public string Currency { get; init; }

    public string ImageUrl { get; init; }
}
