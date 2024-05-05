using FluentValidation;

namespace Ecommerce.Application.SubCategories.DeleteSubCategory;

internal sealed class DeleteSubCategoryCommandValidator : AbstractValidator<DeleteSubCategoryCommand>
{
    public DeleteSubCategoryCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("Invalid identifier.");
    }
}
