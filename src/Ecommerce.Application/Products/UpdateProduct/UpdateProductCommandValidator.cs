using Ecommerce.Domain.Products;
using Ecommerce.Domain.Shared;
using FluentValidation;

namespace Ecommerce.Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id)
           .NotEmpty()
           .WithMessage("Id cannot be empty.")
           .Must(id => Guid.TryParse(id.ToString(), out _))
           .WithMessage("Invalid identifier.");

        RuleFor(c => c.CategoryId)
            .NotEmpty()
            .WithMessage("Category id cannot be empty.")
            .Must(categoryId => Guid.TryParse(categoryId.ToString(), out _))
            .WithMessage("Invalid category identifier.");

        RuleFor(c => c.SubCategoryId)
            .NotEmpty()
            .WithMessage("Subcategory id cannot be empty.")
            .Must(subCategoryId => Guid.TryParse(subCategoryId.ToString(), out _))
            .WithMessage("Invalid sub-category identifier.");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty.");

        RuleFor(c => c.Status)
            .NotEmpty()
            .WithMessage("Status cannot be empty.")
            .Must(DoesStatusExist)
            .WithMessage("Invalid status.");

        RuleFor(c => c.Amount)
            .NotEmpty()
            .WithMessage("Amount cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");

        RuleFor(c => c.Currency)
            .NotEmpty()
            .WithMessage("Currency cannot be empty.")
            .Must(DoesCurrencyExist)
            .WithMessage("Invalid currency.");

        RuleFor(c => c.ImageName)
            .NotEmpty()
            .WithMessage("Image name cannot be empty.");
    }

    private bool DoesStatusExist(string value)
    {
        return !string.IsNullOrWhiteSpace(Status.CheckStatus(value).Value);
    }

    private bool DoesCurrencyExist(string code)
    {
        return !string.IsNullOrWhiteSpace(Currency.CheckCode(code).Code);
    }

}
