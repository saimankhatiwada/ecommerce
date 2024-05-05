using FluentValidation;

namespace Ecommerce.Application.Products.DeleteProduct;

internal sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(c => c.Id)
           .NotEmpty()
           .WithMessage("Id cannot be empty.")
           .Must(id => Guid.TryParse(id.ToString(), out _))
           .WithMessage("Invalid identifier.");
    }
}
