using FluentValidation;

namespace Ecommerce.Application.SubCategories.UpdateSubCategory;

internal sealed class UpdateSubCategoryCommandValidator : AbstractValidator<UpdateSubCategoryCommand>
{
    public UpdateSubCategoryCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid identifier.");

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
