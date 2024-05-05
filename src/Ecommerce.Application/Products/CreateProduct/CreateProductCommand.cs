using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.Products.CreateProduct;

public sealed record CreateProductCommand(
    Guid CategoryId, 
    Guid SubCategoryId,
    string Name,
    string Description,
    string Status,
    decimal Amount,
    string Currency,
    string ImageName,
    string ImageFileContentType,
    Stream ImageStream) : ICommand<Guid>;
