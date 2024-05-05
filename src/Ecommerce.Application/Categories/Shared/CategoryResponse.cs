namespace Ecommerce.Application.Categories.Shared;

public sealed record CategoryResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}
