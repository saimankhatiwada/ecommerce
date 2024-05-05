using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Products.Shared;

namespace Ecommerce.Application.Products.GetProduct;

public sealed record GetProductQuery(Guid Id) : IQuery<ProductResponse>;
