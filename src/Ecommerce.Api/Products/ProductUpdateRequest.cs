namespace Ecommerce.Api.Products;

public sealed record ProductUpdateRequest(
    Guid CategoryId, 
    Guid SubCategoryId,
    string Name,
    string Description,
    string Status,
    decimal Amount,
    string Currency,
    IFormFile ImageFile);
