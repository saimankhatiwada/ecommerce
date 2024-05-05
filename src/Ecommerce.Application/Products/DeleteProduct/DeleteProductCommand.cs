using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : ICommand;
