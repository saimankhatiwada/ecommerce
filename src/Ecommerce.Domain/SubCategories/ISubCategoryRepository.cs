namespace Ecommerce.Domain.SubCategories;

public interface ISubCategoryRepository
{
    Task<SubCategory?> GetByIdAsync(SubCategoryId id, CancellationToken cancellationToken = default);

    void Add(SubCategory subCategory);

    void Delete(SubCategory subCategory);
}
