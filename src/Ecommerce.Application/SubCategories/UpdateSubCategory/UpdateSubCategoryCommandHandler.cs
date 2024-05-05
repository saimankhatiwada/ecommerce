using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Categories;
using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Application.SubCategories.UpdateSubCategory;

internal sealed class UpdateSubCategoryCommandHandler : ICommandHandler<UpdateSubCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateSubCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        ISubCategoryRepository subCategoryRepository, 
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetByIdAsync(new CategoryId(request.CategoryId), cancellationToken);

        if(category is null)
        {
            return Result.Failure(CategoryErrors.NotFound);
        }
        
        SubCategory? subCategory = await _subCategoryRepository.GetByIdAsync(new SubCategoryId(request.Id), cancellationToken);
        
        if(subCategory is null)
        {
            return Result.Failure(SubCategoryErrors.NotFound);
        }

        subCategory.Update(new Domain.SubCategories.Name(request.Name), category.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
