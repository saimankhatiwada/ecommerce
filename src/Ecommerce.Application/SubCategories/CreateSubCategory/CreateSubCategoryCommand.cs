using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.SubCategories.CreateSubCategory;

public sealed record CreateSubCategoryCommand(string Name, Guid CategoryId) : ICommand<Guid>;
