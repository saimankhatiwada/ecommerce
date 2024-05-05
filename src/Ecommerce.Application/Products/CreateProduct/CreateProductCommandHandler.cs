using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Abstractions.Storage;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Categories;
using Ecommerce.Domain.Products;
using Ecommerce.Domain.Shared;
using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly ISubCategoryRepository _subCategoryRepository;

    private readonly IProductRepository _productRepository;

    private readonly IBlobStorage _blobStorage;

    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
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

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetByIdAsync(new CategoryId(request.CategoryId), cancellationToken);

        if(category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound);
        }

        SubCategory? subCategory = await _subCategoryRepository.GetByIdAsync(new SubCategoryId(request.SubCategoryId), cancellationToken);

        if(subCategory is null)
        {
            return Result.Failure<Guid>(SubCategoryErrors.NotFound);
        }

        var product = Product.Create(
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
            product.ImageName.Value, 
            cancellationToken);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id.Value;
        
    }

}
