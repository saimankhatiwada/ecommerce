using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.SubCategories.Shared;

namespace Ecommerce.Application.SubCategories.GetSubCategory;

public sealed record GetSubCategoryQuery(Guid Id) : IQuery<SubCategoryResponse>;
