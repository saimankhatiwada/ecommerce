using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Categories.Shared;

namespace Ecommerce.Application.Categories.GetCategoryName;

public sealed record GetCategoryNameQuery(Guid Id) : IQuery<CategoryNameResponse>;
