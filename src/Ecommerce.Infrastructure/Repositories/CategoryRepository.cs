using Ecommerce.Domain.Categories;

namespace Ecommerce.Infrastructure.Repositories;

internal sealed class CategoryRepository : Repository<Category, CategoryId>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}
