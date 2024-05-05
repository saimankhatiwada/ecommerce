using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.SubCategories.UpdateSubCategory;

public sealed record UpdateSubCategoryCommand(Guid Id, string Name, Guid CategoryId) : ICommand;
