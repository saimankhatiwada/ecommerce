using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Categories.Shared;

namespace Ecommerce.Application.Categories.GetCategory;

public sealed record GetCategoryQuery(Guid Id) : IQuery<CategoryResponse>;
