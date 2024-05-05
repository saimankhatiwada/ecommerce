using FluentValidation;

namespace Ecommerce.Application.Categories.UpdateCategory;

internal sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid identifier.");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");
    }
}
