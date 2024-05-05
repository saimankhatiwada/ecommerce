using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Infrastructure.Repositories;

internal sealed class SubCategoryRepository : Repository<SubCategory, SubCategoryId>, ISubCategoryRepository
{
    public SubCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}
