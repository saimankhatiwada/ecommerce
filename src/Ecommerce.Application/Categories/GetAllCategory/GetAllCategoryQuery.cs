using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Categories.Shared;

namespace Ecommerce.Application.Categories.GetAllCategory;

public sealed record GetAllCategoryQuery() : IQuery<IReadOnlyList<CategoryResponse>>;
