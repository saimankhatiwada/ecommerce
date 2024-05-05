using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Categories;
using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Application.SubCategories.CreateSubCategory;

internal sealed class CreateSubCategoryCommandHandler : ICommandHandler<CreateSubCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateSubCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        ISubCategoryRepository subCategoryRepository, 
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetByIdAsync(new CategoryId(request.CategoryId), cancellationToken);

        if(category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound);
        }
        
        var subCategory = SubCategory.Create(new Domain.SubCategories.Name(request.Name), category.Id);

        _subCategoryRepository.Add(subCategory);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return subCategory.Id.Value;
    }

}
