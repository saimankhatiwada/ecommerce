using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Products.Shared;

namespace Ecommerce.Application.Products.GetAllProduct;

public sealed record GetAllProductQuery() : IQuery<IReadOnlyList<ProductResponse>>;
