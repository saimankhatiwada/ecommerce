using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Abstractions.Storage;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Products;

namespace Ecommerce.Application.Products.DeleteProduct;

internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    private readonly IBlobStorage _blobStorage;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(
        IProductRepository productRepository, 
        IBlobStorage blobStorage, 
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _blobStorage = blobStorage;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(new ProductId(request.Id), cancellationToken);

        if(product is null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        _productRepository.Delete(product);

        await _blobStorage.DeleteAsync(product.ImageName.Value, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
