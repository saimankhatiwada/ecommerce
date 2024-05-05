using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.SubCategories.Shared;

namespace Ecommerce.Application.SubCategories.GetAllSubCategory;

public sealed record GetAllSubCategoryQuery() : IQuery<IReadOnlyList<SubCategoryResponse>>;
