using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
