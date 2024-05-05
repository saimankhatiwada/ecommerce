using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Application.SubCategories.DeleteSubCategory;

internal sealed class DeleteSubCategoryCommandHandler : ICommandHandler<DeleteSubCategoryCommand>
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository, IUnitOfWork unitOfWork)
    {
        _subCategoryRepository = subCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
    {
        SubCategory? subCategory = await _subCategoryRepository.GetByIdAsync(new SubCategoryId(request.Id), cancellationToken);
        
        if(subCategory is null)
        {
            return Result.Failure(SubCategoryErrors.NotFound);
        }

        _subCategoryRepository.Delete(subCategory);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
