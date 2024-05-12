using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.SubCategories.Shared;

namespace Ecommerce.Application.SubCategories.GetSubCategoryName;

public sealed record GetSubCategoryNameQuery(Guid Id) : IQuery<SubCategoryNameResponse>;
