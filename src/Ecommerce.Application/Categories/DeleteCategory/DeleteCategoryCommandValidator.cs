using FluentValidation;

namespace Ecommerce.Application.Categories.DeleteCategory;

internal sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid identifier.");
    }
}
