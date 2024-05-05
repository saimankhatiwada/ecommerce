using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.SubCategories.DeleteSubCategory;

public sealed record DeleteSubCategoryCommand(Guid Id) : ICommand;
