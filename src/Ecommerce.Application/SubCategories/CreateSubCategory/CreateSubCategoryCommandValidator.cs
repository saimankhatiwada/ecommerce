using FluentValidation;

namespace Ecommerce.Application.SubCategories.CreateSubCategory;

internal sealed class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
{
    public CreateSubCategoryCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");

        RuleFor(c => c.CategoryId)
            .NotEmpty()
            .WithMessage("Category id cannot be empty.")
            .Must(categoryId => Guid.TryParse(categoryId.ToString(), out _))
            .WithMessage("Invalid category identifier.");
    }
}
