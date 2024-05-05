using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    Guid CategoryId, 
    Guid SubCategoryId,
    string Name,
    string Description,
    string Status,
    decimal Amount,
    string Currency,
    string ImageName,
    string ImageFileContentType,
    Stream ImageStream) : ICommand;
