using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.Categories.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid Id) : ICommand;
