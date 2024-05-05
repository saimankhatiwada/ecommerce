using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Abstractions.Storage;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Categories;
using Ecommerce.Domain.Products;
using Ecommerce.Domain.Shared;
using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly ISubCategoryRepository _subCategoryRepository;

    private readonly IProductRepository _productRepository;

    private readonly IBlobStorage _blobStorage;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(
        ICategoryRepository categoryRepository, 
        ISubCategoryRepository subCategoryRepository,
        IProductRepository productRepository,
        IBlobStorage blobStorage,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
        _productRepository = productRepository;
        _blobStorage = blobStorage;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetByIdAsync(new CategoryId(request.CategoryId), cancellationToken);

        if(category is null)
        {
            return Result.Failure(CategoryErrors.NotFound);
        }

        SubCategory? subCategory = await _subCategoryRepository.GetByIdAsync(new SubCategoryId(request.SubCategoryId), cancellationToken);

        if(subCategory is null)
        {
            return Result.Failure(SubCategoryErrors.NotFound);
        }

        Product? product = await _productRepository.GetByIdAsync(new ProductId(request.Id), cancellationToken);

        if(product is null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        await _blobStorage.DeleteAsync(product.ImageName.Value, cancellationToken);

        product.Update(
            category.Id, 
            subCategory.Id,
            new Domain.Products.Name(request.Name),
            new Description(request.Description),
            Status.FromStatus(request.Status),
            new Money(request.Amount, Currency.FromCode(request.Currency)),
            new ImageName(request.ImageName));

        await _blobStorage.UploadAsync(
            request.ImageStream, 
            request.ImageFileContentType, 
            request.ImageName, 
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
