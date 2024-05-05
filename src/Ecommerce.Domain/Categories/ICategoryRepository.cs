namespace Ecommerce.Domain.Categories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(CategoryId id, CancellationToken cancellationToken = default);

    void Add(Category category);

    void Delete(Category category);
}
