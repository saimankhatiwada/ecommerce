using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : ICommand;
